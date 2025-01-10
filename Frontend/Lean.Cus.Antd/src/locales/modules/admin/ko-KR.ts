export default {
  user: {
    title: '사용자 관리',
    username: '사용자 이름',
    password: '비밀번호',
    name: '이름',
    email: '이메일',
    phone: '전화번호',
    role: '역할',
    status: '상태',
    createTime: '생성 시간',
    updateTime: '수정 시간',
    lastLoginTime: '마지막 로그인',
    action: {
      add: '사용자 추가',
      edit: '사용자 편집',
      delete: '사용자 삭제',
      resetPassword: '비밀번호 초기화'
    }
  },
  role: {
    title: '역할 관리',
    name: '역할 이름',
    code: '역할 코드',
    description: '설명',
    permissions: '권한',
    action: {
      add: '역할 추가',
      edit: '역할 편집',
      delete: '역할 삭제'
    }
  },
  menu: {
    title: '메뉴 관리',
    name: '메뉴 이름',
    path: '경로',
    component: '컴포넌트',
    icon: '아이콘',
    sort: '정렬 순서',
    parent: '상위 메뉴',
    action: {
      add: '메뉴 추가',
      edit: '메뉴 편집',
      delete: '메뉴 삭제'
    }
  },
  permission: {
    title: '권한 관리',
    name: '권한 이름',
    code: '권한 코드',
    description: '설명',
    action: {
      add: '권한 추가',
      edit: '권한 편집',
      delete: '권한 삭제'
    }
  }
} 