# **ShelfHunter - Buscador de Livros**

**ShelfHunter** é um aplicativo desenvolvido com .NET MAUI que permite pesquisar livros utilizando a API do Google Books. Ele exibe informações como título, autores, editora, descrição e ano de publicação dos livros encontrados, além de permitir a navegação entre os resultados.

---

## **Como baixar o aplicativo?**

Para baixar o **ShelfHunter**, siga os passos abaixo:

1. Clone este repositório em sua máquina local.
2. Navegue até o seguinte diretório:
   ```
   \MauiAppAlexandria\bin\Release\net9.0-windows10.0.19041.0\win10-x64
   ```
3. Localize o arquivo **MauiAppAlexandria.exe**. Este é o executável do aplicativo.
4. Execute o arquivo para iniciar o aplicativo.

---

## **Recursos**

- Pesquisa de livros por:
  - Título
  - Autor
  - Editora
- Exibição de detalhes dos livros, incluindo:
  - Título
  - Autores
  - Editora
  - Descrição
  - Ano de publicação
  - Número de páginas
  - Imagem da capa (quando disponível).
- Interface intuitiva para navegação entre os resultados.

---

## **Tecnologias Utilizadas**

- .NET MAUI
- C#
- API do Google Books

---

## **Requisitos**

- .NET SDK 7.0 ou superior.
- IDE compatível com .NET MAUI (Visual Studio 2022 ou superior).
- Uma chave de API válida do Google Books.

---

## **Estrutura do Projeto**

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

---

## **Funcionalidades**

### **Tela Inicial**

- Permite inserir um ou mais critérios de pesquisa:
  - Nome do livro
  - Nome do autor
  - Editora
- Botão para iniciar a pesquisa.

![Tela Inicial App](https://github.com/user-attachments/assets/357b7062-9dae-4b8b-a4d5-4534ab583b4d)


### **Tela de Resultados**

- Exibe os detalhes do livro selecionado.
- Navegação entre os resultados com botões **"Anterior"** e **"Próximo"**.
- Mostra uma mensagem amigável caso nenhuma informação esteja disponível.

![Tela de resultado da pesquisa do livro App](https://github.com/user-attachments/assets/5b4e971b-8baa-43aa-8036-6a085ef430ce)


---

## **Exemplos de Uso**

### **Pesquisa por Nome do Livro**

- Digite o título do livro e clique em **"Pesquisar Livro"**.
- A aplicação mostrará os resultados compatíveis com o termo inserido.

### **Navegação entre Livros**

- Use os botões **"Anterior"** e **"Próximo"** para alternar entre os resultados encontrados.

### **Exibição de Capa**

- Se o livro possuir uma imagem de capa disponível, ela será exibida na tela de detalhes.
- Caso contrário, uma imagem padrão será mostrada.

---

