git fetch --all
git clean -f
git clean -fd
git reset --hard HEAD
git checkout origin/feature/001_AddingVSProject
git pull origin feature/001_AddingVSProject
git checkout feature/001_AddingVSProject
pause