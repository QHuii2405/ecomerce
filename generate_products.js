const fs = require('fs');
const path = require('path');

const seedDate = "new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc)";

const categories = {
    "c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61": { name: "Laptops", folder: "latop", brands: ["Apple", "Asus", "Dell", "HP", "Lenovo"] },
    "c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64": { name: "Smartphones", folder: "phone", brands: ["Apple", "Samsung", "Xiaomi", "Oppo", "Vivo"] },
    "c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62": { name: "Gaming", folder: "keyboard", brands: ["Logitech", "Razer", "Corsair", "SteelSeries", "HyperX"] },
    "c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63": { name: "Audio", folder: "speaker", brands: ["Sony", "JBL", "Bose", "Marshall", "Sennheiser"] },
    "c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65": { name: "Accessories", folder: "phone case", brands: ["Spigen", "Anker", "UAG", "Nillkin", "Baseus"] }
};

const imagesBaseDir = "d:\\Project\\ecomerce\\src\\WebAPI\\wwwroot\\image";

let productsOutput = `using Domain.Entities;

namespace Infrastructure.Persistence.SeedData;

public static class ProductSeedData
{
    public static (IEnumerable<Product> Products, IEnumerable<Inventory> Inventories, IEnumerable<ProductVariant> Variants) GetSeedData()
    {
        var products = new List<Product>();
        var inventories = new List<Inventory>();
        var variants = new List<ProductVariant>();
        
        var seedDate = ${seedDate};
        
`;

function generateGuid(index) {
    let str = index.toString().padStart(3, '0');
    return `"d0000000-0000-0000-0000-000000000${str}"`;
}
function generateInvGuid(index) {
    let str = index.toString().padStart(3, '0');
    return `"e0000000-0000-0000-0000-000000000${str}"`;
}
function generateVarGuid(index) {
    let str = index.toString().padStart(3, '0');
    return `"a0000000-0000-0000-0000-000000000${str}"`;
}

let productIndex = 1;
let variantIndex = 1;

for (const [catId, catInfo] of Object.entries(categories)) {
    let folderPath = path.join(imagesBaseDir, catInfo.folder);
    let images = [];
    if (fs.existsSync(folderPath)) {
        images = fs.readdirSync(folderPath).filter(f => f.endsWith('.jpg') || f.endsWith('.png') || f.endsWith('.webp'));
    }
    
    let imgIndex = 0;
    
    for (let i = 0; i < 10; i++) {
        let pId = generateGuid(productIndex);
        let iId = generateInvGuid(productIndex);
        
        let brand = catInfo.brands[i % catInfo.brands.length];
        let name = `${brand} Premium ${catInfo.name} Model ${i + 1}`;
        let description = `Đây là sản phẩm ${catInfo.name} cao cấp từ thương hiệu ${brand}. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.`;
        let price = Math.floor(Math.random() * 200 + 10) * 100000; // 1tr to 21tr
        
        // Pick 2 images
        let pImages = [];
        for (let j = 0; j < 2; j++) {
            if (images.length > 0) {
                pImages.push(`"/image/${catInfo.folder}/${images[imgIndex % images.length]}"`);
                imgIndex++;
            }
        }
        if (pImages.length === 0) pImages.push(`""`);
        
        productsOutput += `
        // --- Product ${productIndex} ---
        var p${productIndex}Id = Guid.Parse(${pId});
        products.Add(new Product { 
            Id = p${productIndex}Id, 
            Name = "${name}", 
            Brand = "${brand}", 
            Description = "${description}", 
            Price = ${price}, 
            CategoryId = Guid.Parse("${catId}"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { ${pImages.join(', ')} }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse(${iId}), 
            ProductId = p${productIndex}Id, 
            StockQuantity = ${Math.floor(Math.random() * 50 + 10)}, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        `;
        
        // Add 1-2 variants
        let numVariants = (i % 2 === 0) ? 2 : 1;
        for (let v = 0; v < numVariants; v++) {
            let vId = generateVarGuid(variantIndex);
            let vName = `Phiên bản ${v === 0 ? 'Tiêu chuẩn' : 'Cao cấp'}`;
            let vPrice = price + (v * 1000000);
            let attrJson = `{\\"color\\":\\"${v === 0 ? 'Black' : 'White'}\\"}`;
            
            productsOutput += `
        variants.Add(new ProductVariant { 
            Id = Guid.Parse(${vId}), 
            ProductId = p${productIndex}Id, 
            Sku = "${brand.substring(0,3).toUpperCase()}-${productIndex}-${v}", 
            Name = "${vName}", 
            AttributesJson = "${attrJson}", 
            Price = ${vPrice}, 
            StockQuantity = ${Math.floor(Math.random() * 20 + 5)}, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });`;
            variantIndex++;
        }
        productIndex++;
    }
}

productsOutput += `
        return (products, inventories, variants);
    }
}
`;

fs.writeFileSync('d:\\Project\\ecomerce\\src\\Infrastructure\\Persistence\\SeedData\\ProductSeedData.cs', productsOutput);
console.log("Generated ProductSeedData.cs");
