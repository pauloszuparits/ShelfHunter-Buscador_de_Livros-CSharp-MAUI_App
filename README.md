# ShelfHunter - Buscador de Livros

**ShelfHunter** é um aplicativo desenvolvido com .NET MAUI que permite pesquisar livros utilizando a API do Google Books. Ele apresenta informações como título, autores, editora, descrição e ano de publicação dos livros encontrados, além de permitir a navegação entre os resultados.

## Como baixar o aplicativo?

Para baixar o **ShelfHunter** o usuário deve clicar clonar esse repositorio e ir até o caminho abaixo
\MauiAppAlexandria\bin\Release\net9.0-windows10.0.19041.0\win10-x64

Nessa pasta, terá um arquivo MauiAppAlexandria.exe, que é o executavel deste APP.

## Recursos

- Pesquisa de livros por título, autor ou editora.
- Exibição de detalhes dos livros, incluindo:
  - Título
  - Autores
  - Editora
  - Descrição
  - Ano de publicação
  - Número de páginas
  - Imagem da capa (quando disponível).
- Interface intuitiva para navegação entre os resultados.

## Tecnologias Utilizadas

- .NET MAUI
- C#
- API do Google Books

## Requisitos

- .NET SDK 7.0 ou superior.
- IDE compatível com .NET MAUI (Visual Studio 2022 ou superior).
- Uma chave de API válida do Google Books.


## Estrutura do Projeto

```
MauiApp/
├── Resources/
│   ├── Fonts/                # Fontes personalizadas do aplicativo
│   ├── Images/               # Imagens usadas no layout
├── Models/
│   ├── Book.cs               # Modelo de dados para representar um livro
├── Views/
│   ├── MainPage.xaml         # Página inicial para pesquisa
│   ├── BooksPage.xaml        # Página para exibir os detalhes dos livros
├── ViewModels/               # (Opcional) Camada de ViewModel, se necessário
├── App.xaml                  # Configuração global do aplicativo
├── MauiProgram.cs            # Ponto de entrada do aplicativo .NET MAUI
└── README.md                 # Documentação do projeto
```

## Funcionalidades

### Tela Inicial
- Permite inserir um ou mais critérios de pesquisa:
  - Nome do livro
  - Nome do autor
  - Editora
- Botão para iniciar a pesquisa.

### Tela de Resultados
- Exibe os detalhes do livro selecionado.
- Navegação entre os resultados com botões "Anterior" e "Próximo".
- Mostra uma mensagem amigável caso nenhuma informação esteja disponível.

## Exemplos de Uso

### Pesquisa por Nome do Livro
Digite o título do livro e clique em "Pesquisar Livro". A aplicação mostrará os resultados compatíveis com o termo inserido.

### Navegação entre Livros
Use os botões **Anterior** e **Próximo** para alternar entre os resultados encontrados.

### Exibição de Capa
Se o livro possuir uma imagem de capa disponível, ela será exibida na tela de detalhes. Caso contrário, uma imagem padrão será mostrada.

## Melhorias Futuras

- Adicionar suporte para salvar livros favoritos.
- Implementar paginação nos resultados da pesquisa.
- Melhorar o tratamento de erros e mensagens ao usuário.

