git fetch --all
git clean -f
git clean -fd
git reset --hard HEAD
git checkout origin/feature/009_PATCH/Demo
git pull origin feature/009_PATCH/Demo
git checkout feature/009_PATCH/Demo
pause