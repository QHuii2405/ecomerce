module.exports = function(fileInfo, api) {
    const j = api.jscodeshift;
    const root = j(fileInfo.source);
    let hasAlert = false;
    let hasConfirm = false;

    // Find alert(...) calls
    root.find(j.CallExpression, {
        callee: { name: 'alert' }
    }).forEach(path => {
        hasAlert = true;
        const arg = path.node.arguments[0];
        const swalCall = j.callExpression(
            j.memberExpression(j.identifier('Swal'), j.identifier('fire')),
            [j.objectExpression([
                j.property('init', j.identifier('icon'), j.literal('info')),
                j.property('init', j.identifier('text'), arg)
            ])]
        );
        j(path).replaceWith(swalCall);
    });

    // Find window.confirm(...) calls
    root.find(j.CallExpression, {
        callee: {
            type: 'MemberExpression',
            object: { name: 'window' },
            property: { name: 'confirm' }
        }
    }).forEach(path => {
        hasConfirm = true;
        const arg = path.node.arguments[0];
        let parent = path.parent;
        while(parent && parent.node.type !== 'ArrowFunctionExpression' && parent.node.type !== 'FunctionDeclaration' && parent.node.type !== 'FunctionExpression') {
            parent = parent.parent;
        }
        if (parent) {
            parent.node.async = true;
        }

        const swalCall = j.awaitExpression(
            j.callExpression(
                j.memberExpression(j.identifier('Swal'), j.identifier('fire')),
                [j.objectExpression([
                    j.property('init', j.identifier('title'), j.literal('Xác nh?n')),
                    j.property('init', j.identifier('text'), arg),
                    j.property('init', j.identifier('icon'), j.literal('warning')),
                    j.property('init', j.identifier('showCancelButton'), j.booleanLiteral(true)),
                    j.property('init', j.identifier('confirmButtonText'), j.literal('Ð?ng ý')),
                    j.property('init', j.identifier('cancelButtonText'), j.literal('H?y'))
                ])]
            )
        );
        const isConfirmed = j.memberExpression(
            swalCall, // Actually jscodeshift handles parens based on precedence, but j.parenthesizedExpression(swalCall) is safer? AST printer handles it.
            j.identifier('isConfirmed')
        );
        j(path).replaceWith(isConfirmed);
    });

    if (hasAlert || hasConfirm) {
        const imports = root.find(j.ImportDeclaration);
        let hasSwalImport = false;
        imports.forEach(p => {
            if (p.node.source.value === 'sweetalert2') hasSwalImport = true;
        });

        if (!hasSwalImport) {
            const swalImport = j.importDeclaration(
                [j.importDefaultSpecifier(j.identifier('Swal'))],
                j.literal('sweetalert2')
            );
            if (imports.length > 0) {
                j(imports.at(0).get()).insertBefore(swalImport);
            } else {
                root.get().node.program.body.unshift(swalImport);
            }
        }
        return root.toSource();
    }
    return null;
};
