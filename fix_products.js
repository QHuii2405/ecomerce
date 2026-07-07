
const fs = require('fs');
let content = fs.readFileSync('src/Infrastructure/Persistence/SeedData/ProductSeedData.cs', 'utf8');
let i = 0;
content = content.replace(/Price = (\d+),[\s\S]*?IsDeleted = false, IsNew = true, OldPrice = null,/g, (match, price) => {
    i++;
    const isNew = (i % 3 === 0) ? 'true' : 'false';
    const oldPrice = (i % 4 === 1) ? (parseInt(price) * 1.2).toString() + 'm' : 'null';
    return match.replace('IsDeleted = false, IsNew = true, OldPrice = null,', 'IsDeleted = false, IsNew = ' + isNew + ', OldPrice = ' + oldPrice + ',');
});
fs.writeFileSync('src/Infrastructure/Persistence/SeedData/ProductSeedData.cs', content);

