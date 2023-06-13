# Group Study Project
PLEASE READ THIS FILE CAREFULLY!
## EF migration code first
To apply the latest migrations to your physical database, run this command(run the command from group-study-system folder)
```
dotnet ef database update -s RazorPageWebApp -p DataAccess
```
## Branch convention
- For every new function, you must create a new branch, then invite other members to review your code in Git Hub, when that branch merged successfully, you can delete it and continue to create a new branch based on main for other functions.
- When you create a new branch, it must be based on the **main branch** and follow this naming convention:
**"YourName_FunctionName"**
## Commit naming convention
When you add a new commit, it must follow this naming convention:
**"[YourName][Action description]"**
## Pull request convention
You must create a pull request before your code is merged into main. The pull request must be followed this naming convention:
**"[YourName][YourFunction]"**
## PRN221 - Group 6
1. Vo Ngoc Truc Lam
2. Nguyen Thanh Binh
3. Huynh Van Phu