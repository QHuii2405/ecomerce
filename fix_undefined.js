const fs = require('fs');
const path = require('path');

function getFiles(dir, ext, fileList = []) {
    const files = fs.readdirSync(dir);
    for (const file of files) {
        const filePath = path.join(dir, file);
        if (fs.statSync(filePath).isDirectory()) {
            getFiles(filePath, ext, fileList);
        } else if (filePath.endsWith(ext) || filePath.endsWith(ext + 'x')) {
            fileList.push(filePath);
        }
    }
    return fileList;
}

const srcDir = path.join(__dirname, 'ecommerce-web', 'src');
const files = getFiles(srcDir, '.js');

files.forEach(file => {
    let content = fs.readFileSync(file, 'utf8');
    if (content.includes('import.meta.env.VITE_API_BASE_URL')) {
        content = content.replace(/\(import\.meta\.env\.VITE_API_BASE_URL \|\| ''\)/g, 'import.meta.env.VITE_API_BASE_URL');
        content = content.replace(/import\.meta\.env\.VITE_API_BASE_URL/g, "(import.meta.env.VITE_API_BASE_URL || '')");
        fs.writeFileSync(file, content, 'utf8');
        console.log(`Updated ${file}`);
    }
});
