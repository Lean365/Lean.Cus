import * as echarts from 'echarts/core';
import {
  BarChart,
  LineChart,
  PieChart,
  ScatterChart,
  RadarChart,
  GaugeChart,
} from 'echarts/charts';
import {
  TitleComponent,
  TooltipComponent,
  GridComponent,
  DatasetComponent,
  TransformComponent,
  LegendComponent,
  ToolboxComponent,
} from 'echarts/components';
import { LabelLayout, UniversalTransition } from 'echarts/features';
import { CanvasRenderer } from 'echarts/renderers';

// 注册必须的组件
echarts.use([
  BarChart,
  LineChart,
  PieChart,
  ScatterChart,
  RadarChart,
  GaugeChart,
  TitleComponent,
  TooltipComponent,
  GridComponent,
  DatasetComponent,
  TransformComponent,
  LegendComponent,
  ToolboxComponent,
  LabelLayout,
  UniversalTransition,
  CanvasRenderer,
]);

export class EChartsService {
  private static instances = new Map<string, echarts.ECharts>();

  /**
   * 初始化 ECharts 实例
   * @param element DOM 元素或元素 ID
   * @param theme 主题
   * @returns ECharts 实例
   */
  static init(element: HTMLElement | string, theme?: string): echarts.ECharts {
    const dom = typeof element === 'string' ? document.getElementById(element) : element;
    if (!dom) {
      throw new Error('Element not found');
    }

    // 如果已存在实例，先销毁
    const existingInstance = this.instances.get(dom.id);
    if (existingInstance) {
      existingInstance.dispose();
    }

    // 创建新实例
    const instance = echarts.init(dom, theme);
    if (dom.id) {
      this.instances.set(dom.id, instance);
    }

    // 自动调整大小
    window.addEventListener('resize', () => {
      instance.resize();
    });

    return instance;
  }

  /**
   * 获取 ECharts 实例
   * @param id 元素 ID
   * @returns ECharts 实例
   */
  static getInstance(id: string): echarts.ECharts | undefined {
    return this.instances.get(id);
  }

  /**
   * 销毁 ECharts 实例
   * @param element DOM 元素或元素 ID
   */
  static dispose(element: HTMLElement | string): void {
    const id = typeof element === 'string' ? element : element.id;
    const instance = this.instances.get(id);
    if (instance) {
      instance.dispose();
      this.instances.delete(id);
    }
  }

  /**
   * 销毁所有 ECharts 实例
   */
  static disposeAll(): void {
    this.instances.forEach((instance) => {
      instance.dispose();
    });
    this.instances.clear();
  }

  /**
   * 获取默认配置
   * @returns ECharts 默认配置
   */
  static getDefaultOptions(): echarts.EChartsOption {
    return {
      title: {
        textStyle: {
          fontSize: 14,
          fontWeight: 'normal',
        },
      },
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'shadow',
        },
      },
      grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        containLabel: true,
      },
      toolbox: {
        feature: {
          saveAsImage: {},
          dataView: {},
          restore: {},
        },
      },
    };
  }

  /**
   * 合并配置
   * @param target 目标配置
   * @param source 源配置
   * @returns 合并后的配置
   */
  static mergeOptions(
    target: echarts.EChartsOption,
    source: echarts.EChartsOption
  ): echarts.EChartsOption {
    return echarts.util.merge(target, source, true);
  }

  /**
   * 创建渐变色
   * @param colors 颜色数组
   * @param direction 渐变方向 ('horizontal' | 'vertical')
   * @returns 渐变色对象
   */
  static createLinearGradient(
    colors: string[],
    direction: 'horizontal' | 'vertical' = 'vertical'
  ): echarts.LinearGradientObject {
    const isHorizontal = direction === 'horizontal';
    return new echarts.graphic.LinearGradient(
      isHorizontal ? 0 : 0,
      isHorizontal ? 0 : 0,
      isHorizontal ? 1 : 0,
      isHorizontal ? 0 : 1,
      colors.map((color, index) => ({
        offset: index / (colors.length - 1),
        color,
      }))
    );
  }
} 