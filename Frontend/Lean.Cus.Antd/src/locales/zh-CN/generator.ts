export default {
  generator: {
    title: '代码生成',
    template: {
      title: '模板管理',
      type: '模板类型',
      fileName: '文件名',
      content: '模板内容',
      remark: '备注',
      create: '新增模板',
      update: '编辑模板',
      delete: '删除模板',
      preview: '预览模板'
    },
    design: {
      title: '表设计',
      basic: {
        title: '基本信息',
        tableName: '表名',
        tableComment: '表注释',
        className: '类名',
        author: '作者',
        remark: '备注'
      },
      generate: {
        title: '生成信息',
        templateType: '模板类型',
        frontTemplate: '前端模板',
        namespace: '命名空间',
        businessName: '业务名',
        functionName: '功能名',
        moduleName: '模块名',
        parentMenu: '上级菜单',
        orderByColumn: '排序字段',
        orderType: '排序方式',
        useSnowflake: '使用雪花ID',
        genType: '生成方式',
        columnCount: '列数量',
        genFunctions: '生成功能',
        trackDiffDate: '跟踪差异日期'
      },
      column: {
        title: '字段信息',
        columnName: '字段名',
        columnComment: '字段注释',
        physicalType: '物理类型',
        csharpType: 'C#类型',
        csharpField: 'C#字段',
        isRequired: '必填',
        isGrid: '列表',
        isSort: '排序',
        isEdit: '编辑',
        editType: '编辑类型',
        isExport: '导出',
        isQuery: '查询',
        queryType: '查询类型',
        displayType: '显示类型',
        dictType: '字典类型',
        remark: '备注'
      }
    },
    import: {
      title: '导入表',
      tableName: '表名',
      tableComment: '表注释',
      createTime: '创建时间',
      updateTime: '更新时间'
    },
    preview: {
      title: '预览代码',
      generate: '生成代码',
      download: '下载代码'
    },
    message: {
      confirmDelete: '确定要删除吗？',
      createSuccess: '创建成功',
      updateSuccess: '更新成功',
      deleteSuccess: '删除成功',
      generateSuccess: '生成成功',
      importSuccess: '导入成功'
    }
  }
} 