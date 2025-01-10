export default {
  title: '코드 생성',
  template: {
    title: '템플릿 관리',
    add: '템플릿 추가',
    edit: '템플릿 편집',
    delete: '템플릿 삭제',
    name: '템플릿 이름',
    type: '템플릿 유형',
    content: '템플릿 내용',
    description: '설명',
    types: {
      entity: '엔티티',
      service: '서비스',
      controller: '컨트롤러'
    }
  },
  designer: {
    title: '테이블 설계',
    table: {
      name: '테이블 이름',
      comment: '주석',
      columns: '컬럼'
    },
    column: {
      name: '컬럼 이름',
      type: '데이터 타입',
      length: '길이',
      nullable: 'NULL 허용',
      primary: '기본 키',
      comment: '주석'
    }
  },
  generate: {
    title: '코드 생성',
    preview: '미리보기',
    download: '다운로드',
    success: '생성 성공',
    failed: '생성 실패'
  }
} 