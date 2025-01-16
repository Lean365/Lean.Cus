<template>
  <div class="captcha-container">
    <div class="background-wrapper">
      <div class="background-image">
        <img :src="bgImage" class="bg-img" />
        <img 
          :src="slideImage" 
          class="slider-image"
          :style="{ 
            top: `${sliderY}px`,
            left: `${sliderPosition}px`,
            transform: 'none'
          }"
        />
      </div>
    </div>
    <div class="slider-container">
      <div class="slider-track">
        <div 
          class="slider-progress"
          :style="{ width: `${sliderPosition}px`, transform: 'none' }"
        ></div>
        <div class="slider-text">向右滑动完成拼图</div>
        <div 
          class="slider-handle" 
          :style="{ left: `${sliderPosition}px`, transform: 'none' }"
          @mousedown="handleMouseDown"
          @touchstart.prevent="handleMouseDown"
        >
          <div class="arrow-icon">
            <svg viewBox="0 0 24 24" width="16" height="16">
              <path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z" fill="#666"/>
            </svg>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { message } from 'ant-design-vue'
import { generateCaptcha, verifyCaptcha } from '@/api/auth'

// 组件属性定义
const props = defineProps<{
  onSuccess: (code: string, uuid: string) => void
  onFail?: () => void
}>()

// 验证码相关状态
const sliderValue = ref(0)
const sliderY = ref(0)
const bgImage = ref('')
const slideImage = ref('')
const uuid = ref('')

// 背景图和滑块的尺寸配置
const BACKGROUND_WIDTH = 280
const BACKGROUND_HEIGHT = 155
const SLIDER_WIDTH = 55

// 计算滑块图片的实际位置
const sliderPosition = ref(0)

// 获取验证码
const getCaptcha = async () => {
  try {
    const res = await generateCaptcha()
    if (!res.data) {
      throw new Error('获取验证码失败')
    }
    
    const { uuid: newUuid, backgroundImage, sliderImage: newSliderImage, y } = res.data
    if (!backgroundImage || !newSliderImage) {
      throw new Error('验证码图片数据无效')
    }

    uuid.value = newUuid
    bgImage.value = `data:image/png;base64,${backgroundImage}`
    slideImage.value = `data:image/png;base64,${newSliderImage}`
    sliderY.value = y
    sliderValue.value = 0
  } catch (error) {
    console.error('获取验证码失败:', error)
    message.error('获取验证码失败')
  }
}

// 验证码验证
const onSliderAfterChange = async () => {
  try {
    const position = sliderPosition.value
    const verifyData = {
      uuid: uuid.value,
      x: position,
      y: sliderY.value
    }
    
    const res = await verifyCaptcha(verifyData)
    if (!res.data) {
      throw new Error('验证失败')
    }

    if (res.data.success) {
      // 等待新的 CSRF token 设置完成
      await new Promise(resolve => setTimeout(resolve, 1000))
      
      // 获取新的CSRF token
      const csrfToken = document.cookie.match(/XSRF-TOKEN=(.*?)(;|$)/)?.[1]
      if (!csrfToken) {
        throw new Error('获取CSRF token失败')
      }
      
      // 验证成功,调用登录
      props.onSuccess(position.toString(), uuid.value)
    } else {
      message.error(res.data.message || '验证失败')
      props.onFail?.()
      await getCaptcha()
    }
  } catch (error: any) {
    console.error('验证失败:', error)
    if (error.response) {
      message.error(error.response.data || '验证失败')
    } else {
      message.error('验证失败')
    }
    props.onFail?.()
    await getCaptcha()
  }
}

// 添加拖动相关逻辑
const isDragging = ref(false)
const startX = ref(0)

const handleMouseDown = (e: MouseEvent | TouchEvent) => {
  e.preventDefault()
  isDragging.value = true
  const clientX = e instanceof MouseEvent ? e.clientX : e.touches[0].clientX
  startX.value = clientX - sliderPosition.value
  
  if (e instanceof MouseEvent) {
    document.addEventListener('mousemove', handleMouseMove)
    document.addEventListener('mouseup', handleMouseUp)
  } else {
    document.addEventListener('touchmove', handleTouchMove, { passive: false })
    document.addEventListener('touchend', handleTouchEnd)
  }
}

const handleMouseMove = (e: MouseEvent) => {
  if (!isDragging.value) return
  handleMove(e.clientX)
}

const handleTouchMove = (e: TouchEvent) => {
  if (!isDragging.value) return
  handleMove(e.touches[0].clientX)
}

const handleMove = (clientX: number) => {
  if (!isDragging.value) return
  // 计算新位置
  const newPosition = clientX - startX.value
  // 限制范围
  const position = Math.min(Math.max(0, newPosition), BACKGROUND_WIDTH - SLIDER_WIDTH)
  // 直接更新位置
  sliderPosition.value = position
}

const handleMouseUp = () => {
  handleEnd()
  document.removeEventListener('mousemove', handleMouseMove)
  document.removeEventListener('mouseup', handleMouseUp)
}

const handleTouchEnd = () => {
  handleEnd()
  document.removeEventListener('touchmove', handleTouchMove)
  document.removeEventListener('touchend', handleTouchEnd)
}

const handleEnd = () => {
  if (isDragging.value) {
    isDragging.value = false
    onSliderAfterChange()
  }
}

onUnmounted(() => {
  document.removeEventListener('mousemove', handleMouseMove)
  document.removeEventListener('mouseup', handleMouseUp)
  document.removeEventListener('touchmove', handleTouchMove)
  document.removeEventListener('touchend', handleTouchEnd)
})

// 初始化
getCaptcha()
</script>

<style lang="less" scoped>
.captcha-container {
  width: 280px;
}

.background-wrapper {
  width: 280px;
  height: 155px;
  background: #fff;
  border-radius: 4px;
  overflow: hidden;
}

.background-image {
  position: relative;
  width: 100%;
  height: 100%;
  
  img.bg-img {
    width: 100%;
    height: 100%;
    display: block;
  }
  
  img.slider-image {
    position: absolute;
    width: 55px;
    height: 55px;
    display: block;
  }
}

.slider-container {
  width: 280px;
  margin: 16px auto 0;
}

.slider-track {
  position: relative;
  height: 40px;
  background: #f7f9fa;
  border: 1px solid #e4e7eb;
  border-radius: 2px;
  user-select: none;
  overflow: hidden;
}

.slider-progress {
  position: absolute;
  top: 0;
  left: 0;
  height: 100%;
  background: #d1e9ff;
  z-index: 2;
  transition: none;
  will-change: width;
}

.slider-handle {
  position: absolute;
  top: 0;
  left: 0;
  width: 40px;
  height: 38px;
  background: #fff;
  border: 1px solid #e4e7eb;
  border-radius: 2px;
  cursor: grab;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: none;
  z-index: 10;
  will-change: transform;
  
  &:active {
    cursor: grabbing;
    border-color: #1890ff;
    box-shadow: 0 0 8px rgba(24, 144, 255, 0.4);
    
    .arrow-icon {
      animation: arrowMove 1s infinite;
    }
  }
  
  .arrow-icon {
    display: flex;
    align-items: center;
    justify-content: center;
    pointer-events: none;
    
    svg {
      transition: transform 0.2s;
    }
  }
  
  &:hover {
    border-color: #1890ff;
    box-shadow: 0 0 4px rgba(24, 144, 255, 0.2);
    
    .arrow-icon svg {
      transform: translateX(3px);
    }
    
    .arrow-icon svg path {
      fill: #1890ff;
    }
  }
}

@keyframes arrowMove {
  0%, 100% {
    transform: translateX(0);
  }
  50% {
    transform: translateX(3px);
  }
}

.slider-text {
  position: absolute;
  width: 100%;
  height: 100%;
  text-align: center;
  line-height: 40px;
  color: #45494c;
  font-size: 14px;
  user-select: none;
  pointer-events: none;
  z-index: 1;
}
</style> 