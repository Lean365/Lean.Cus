# 获取最后一次提交的日期
$lastCommitDate = git log -1 --format=%cd --date=short

# 获取最近的提交记录
$commits = git log --since="1 week ago" --pretty=format:"- %s (%h) <%an>" --reverse

# 读取现有的CHANGELOG.md内容
$changelogContent = Get-Content -Path "CHANGELOG.md" -Raw

# 生成新的变更记录
$newChangelog = @"
## [Unreleased] - $lastCommitDate

### 最近更新
$commits

$changelogContent
"@

# 保存更新后的CHANGELOG.md
$newChangelog | Set-Content -Path "CHANGELOG.md" -Force

Write-Host "CHANGELOG.md 已更新!" 