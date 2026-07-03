const fs = require('fs');
const path = require('path');

const targetFiles = [
    'App.jsx',
    'pages/admin/AdminCategories.jsx',
    'pages/admin/AdminInventory.jsx',
    'pages/admin/AdminOrders.jsx',
    'pages/admin/AdminProducts.jsx',
    'pages/admin/AdminUsers.jsx',
    'pages/admin/AdminVouchers.jsx',
    'pages/admin/GoodsReceiptModal.jsx',
    'pages/admin/ProductModal.jsx',
    'pages/ProductDetail.jsx',
    'pages/Products.jsx',
    'pages/Profile.jsx',
    'pages/Register.jsx'
];

let updatedCount = 0;
targetFiles.forEach(file => {
    let filePath = path.join(__dirname, file);
    if (!fs.existsSync(filePath)) return;
    let content = fs.readFileSync(filePath, 'utf8');
    let originalContent = content;
    
    // Replace alert(...)
    // Simple regex: alert( followed by anything EXCEPT parenthesis, then )
    content = content.replace(/alert\(([^)]+)\)/g, (match, p1) => {
        return `Swal.fire({ icon: 'info', text: ${p1} })`;
    });

    // Replace window.confirm(...)
    // Warning: this assumes it's inside an async function! If not, it will break.
    // I'll skip confirm replacement to avoid breaking non-async functions for now.
    // But let's replace confirm with a non-blocking Swal if we can? No, Swal.fire is async.
    // Let's just do alert.

    if (content !== originalContent) {
        if (!content.includes("import Swal from 'sweetalert2'")) {
            content = "import Swal from 'sweetalert2';\n" + content;
        }
        fs.writeFileSync(filePath, content);
        updatedCount++;
        console.log(`Updated ${file}`);
    }
});
console.log(`Total files updated: ${updatedCount}`);
