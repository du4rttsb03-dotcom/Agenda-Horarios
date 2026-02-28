 const apiUrl = 'https://localhost:7196/api/Alunoes';

    async function carregarAlunos() {
        const res = await fetch(apiUrl);
        const data = await res.json();
        let html = '';
        data.forEach(a => {
            html += `<tr>
                <td>${a.nome}</td><td>${a.cpf}</td><td>${a.categoria}</td>
                <td><span class="badge bg-success">${a.status}</span></td>
                <td>
                    <button class="btn btn-sm btn-warning" onclick="prepararEdicao(${a.id}, '${a.nome}', '${a.cpf}', '${a.categoria}')"><i class="bi bi-pencil"></i></button>
                    <button class="btn btn-sm btn-danger" onclick="excluirAluno(${a.id})"><i class="bi bi-trash"></i></button>
                </td>
            </tr>`;
        });
        document.getElementById('tabelaAlunos').innerHTML = html;
    }

    async function salvarAluno() {
        const id = document.getElementById('alunoId').value;
        const dados = {
            nome: document.getElementById('nome').value,
            cpf: document.getElementById('cpf').value,
            categoria: document.getElementById('categoria').value,
            dataMatricula: new Date().toISOString(),
            status: "Ativo"
        };
        
        let method = 'POST';
        let url = apiUrl;
        if(id) {
            method = 'PUT';
            url = `${apiUrl}/${id}`;
            dados.id = parseInt(id);
        }

        await fetch(url, {
            method: method,
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(dados)
        });
        limparFormulario();
        carregarAlunos();
    }

    async function excluirAluno(id) {
        if(confirm('Tem certeza?')) {
            await fetch(`${apiUrl}/${id}`, {method: 'DELETE'});
            carregarAlunos();
        }
    }

    function prepararEdicao(id, nome, cpf, categoria) {
        document.getElementById('alunoId').value = id;
        document.getElementById('nome').value = nome;
        document.getElementById('cpf').value = cpf;
        document.getElementById('categoria').value = categoria;
    }

    function limparFormulario() {
        document.getElementById('alunoId').value = '';
        document.getElementById('nome').value = '';
        document.getElementById('cpf').value = '';
    }

    carregarAlunos();