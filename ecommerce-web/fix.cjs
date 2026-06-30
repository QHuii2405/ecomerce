const fs = require('fs');
const path = require('path');

const win1252ToByte = {
  '\u20AC': 0x80, '\u201A': 0x82, '\u0192': 0x83, '\u201E': 0x84,
  '\u2026': 0x85, '\u2020': 0x86, '\u2021': 0x87, '\u02C6': 0x88,
  '\u2030': 0x89, '\u0160': 0x8A, '\u2039': 0x8B, '\u0152': 0x8C,
  '\u017D': 0x8E, '\u2018': 0x91, '\u2019': 0x92, '\u201C': 0x93,
  '\u201D': 0x94, '\u2022': 0x95, '\u2013': 0x96, '\u2014': 0x97,
  '\u02DC': 0x98, '\u2122': 0x99, '\u0161': 0x9A, '\u203A': 0x9B,
  '\u0153': 0x9C, '\u017E': 0x9E, '\u0178': 0x9F
};

function tryFixChunk(chunk) {
    const hasHigh = /[\x80-\xFF\u20AC\u201A\u0192\u201E\u2026\u2020\u2021\u02C6\u2030\u0160\u2039\u0152\u017D\u2018\u2019\u201C\u201D\u2022\u2013\u2014\u02DC\u2122\u0161\u203A\u0153\u017E\u0178]/.test(chunk);
    if (!hasHigh) return chunk;
    
    let bytes = [];
    for (let i = 0; i < chunk.length; i++) {
        let char = chunk[i];
        let code = chunk.charCodeAt(i);
        if (win1252ToByte[char] !== undefined) {
            bytes.push(win1252ToByte[char]);
        } else {
            bytes.push(code);
        }
    }
    
    let decoded = Buffer.from(bytes).toString('utf8');
    if (decoded.includes('\uFFFD')) return chunk;
    return decoded;
}

function fixMojibake(content) {
    let result = '';
    let currentChunk = '';

    for (let i = 0; i < content.length; i++) {
        let char = content[i];
        let code = content.charCodeAt(i);
        let isWin1252 = code < 256 || win1252ToByte[char] !== undefined;
        
        if (isWin1252) {
            currentChunk += char;
        } else {
            if (currentChunk.length > 0) {
                result += tryFixChunk(currentChunk);
                currentChunk = '';
            }
            result += char;
        }
    }
    
    if (currentChunk.length > 0) {
        result += tryFixChunk(currentChunk);
    }
    
    return result;
}

const filesToFix = ['src/App.jsx', 'src/pages/Cart.jsx'];

filesToFix.forEach(f => {
    let fullPath = path.join(__dirname, f);
    let content = fs.readFileSync(fullPath, 'utf8');
    let fixedContent = fixMojibake(content);
    fixedContent = fixedContent.replace(/Lumina Tech/g, 'iLuminaty Shop');
    fs.writeFileSync(fullPath, fixedContent, 'utf8');
    console.log('Fixed', f);
});
