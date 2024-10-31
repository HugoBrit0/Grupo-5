CREATE TABLE Anuncios (
    Id INT PRIMARY KEY IDENTITY(1,1),  -- Chave primária com incremento automático
    Titulo NVARCHAR(100) NOT NULL,     -- Título do anúncio
    Descricao NVARCHAR(MAX) NOT NULL,  -- Descrição do anúncio
    ImagePath NVARCHAR(MAX) NULL        -- Caminho da imagem (opcional)
);
