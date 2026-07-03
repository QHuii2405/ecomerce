const { execSync } = require('child_process');
const fs = require('fs');
const path = require('path');

function walkDir(dir, callback) {
    fs.readdirSync(dir).forEach(f => {
        let dirPath = path.join(dir, f);
        let isDirectory = fs.statSync(dirPath).isDirectory();
        isDirectory ? walkDir(dirPath, callback) : callback(path.join(dir, f));
    });
}

const files = ['src/App.jsx'];
walkDir('src/pages', f => { if(f.endsWith('.jsx')) files.push(f.replace(/\\/g, '/')) });

console.log("Running jscodeshift on: " + files.join(' '));
try {
    execSync('npx jscodeshift -t replace-alert.cjs ' + files.join(' ') + ' --parser=tsx', { stdio: 'inherit' });
} catch(e) {
    console.error(e);
}
