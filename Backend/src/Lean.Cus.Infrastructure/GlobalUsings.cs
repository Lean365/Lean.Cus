//===================================================
// 项目名称：Lean.Cus.Infrastructure
// 文件名称：GlobalUsings
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：全局 using 声明
//===================================================

/// <summary>
/// 全局 using 声明
/// <para>为整个基础设施层项目提供通用的命名空间引用</para>
/// <para>避免在每个文件中重复编写相同的 using 语句</para>
/// </summary>

// 系统基础命名空间
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;

// 公共常量和枚举命名空间
global using Lean.Cus.Common.Constants;

// 领域层命名空间

// 数据访问相关命名空间
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;