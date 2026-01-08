# 🚗 VeiculeSale API

![Badge .NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![Badge Docker](https://img.shields.io/badge/Docker-Enabled-2496ED?logo=docker&logoColor=white)
![Badge Kubernetes](https://img.shields.io/badge/Kubernetes-Ready-326CE5?logo=kubernetes&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-4169E1?logo=postgresql&logoColor=white)

> API robusta para gestão de vendas de veículos, implementada com **Clean Architecture**, **DDD** e **CQRS**.

---

## 📑 Índice
- [📍 Visão Geral](#-visão-geral)
- [🏗 Arquitetura e Design](#-arquitetura-e-design)
- [🛠 Tecnologias](#-tecnologias)
- [🚀 Como Rodar (Kubernetes)](#-como-rodar-kubernetes--minikube)
- [⚡ Guia Rápido de Teste (cURL)](#-guia-rápido-de-teste-curl-fluxo-completo)
- [💓 Monitoramento (Health)](#-monitoramento)

---

## 📍 Visão Geral
O **VeiculeSale** gerencia todo o ciclo de vida de uma concessionária digital. O sistema garante a integridade das transações, impedindo vendas de veículos indisponíveis e orquestrando pagamentos via Webhook.

**Principais Funcionalidades:**
* ✅ Cadastro de Veículos e Clientes.
* ✅ Processamento de Venda (Lock otimista de estoque).
* ✅ Integração Assíncrona de Pagamentos (Webhook).
* ✅ Histórico de Vendas.

---

## 🏗 Arquitetura e Design

O projeto foi desenhado para ser desacoplado e testável, seguindo rigorosamente os princípios SOLID.

| Camada | Responsabilidade |
| :--- | :--- |
| **API** | Entry point, Controllers, Swagger e HealthChecks. |
| **Application** | Casos de uso (Handlers), CQRS, DTOs e Interfaces. |
| **Domain** | Entidades, Value Objects, Enums e Regras de Negócio "Core". |
| **Infrastructure** | Implementação de Repositórios (EF Core), Mapeamentos e configs de Banco. |

![Arquitetura](docs/arquitetura.png) 

---

## 🛠 Tecnologias

* **.NET 8**
* **Entity Framework Core** (Code First)
* **PostgreSQL**
* **MediatR** (Padrão Mediator)
* **Docker & Kubernetes** (Infraestrutura)
* **Swagger/OpenAPI** (Documentação)
* **XUnit** (Testes de Unidade)

---

## 🚀 Como Rodar (Kubernetes / Minikube)

Siga os passos abaixo para subir a infraestrutura completa.

### 1. Iniciar o Cluster
```bash
minikube start
```

### 2. Build da Imagem (Passo Crítico ⚠️)
Como o cluster roda localmente com a política `imagePullPolicy: Never`, precisamos construir a imagem dentro do Docker do Minikube:

```bash
minikube image build -t veiculesale:latest -f src/API/Dockerfile .
```

### 3. Aplicar Manifestos
Execute na ordem para garantir as dependências:

```bash
# 1. Configurações (ConfigMap e Secrets)
kubectl apply -f k8s/configmap.yaml
kubectl apply -f k8s/secrets.yaml

# 2. Banco de Dados
kubectl apply -f k8s/postgres.yaml

# 3. Aplicação API
kubectl apply -f k8s/api.yaml
```

### 4. Acessar a Aplicação
Abra um túnel para expor a porta 8080:
```bash
kubectl port-forward svc/veiculesale-service 8080:80
```

📍 **Swagger Disponível em:** [http://localhost:8080/swagger](http://localhost:8080/swagger)

---

## ⚡ Guia Rápido de Teste (cURL: Fluxo Completo)

Abaixo estão os comandos para simular um fluxo de venda completo ("Caminho Feliz").
*Requisito: Terminal com cURL (Git Bash, WSL, Linux ou PowerShell).*

### Passo 1: Cadastrar Veículo
```bash
curl -X 'POST' \
  'http://localhost:8080/api/veiculos' \
  -H 'Content-Type: application/json' \
  -d '{
  "marca": "Honda",
  "modelo": "Civic Touring",
  "ano": 2023,
  "cor": "Branco",
  "preco": 150000
}'
```
> 📝 **Anote o ID** retornado (ex: `uuid-veiculo`).

### Passo 2: Cadastrar Cliente
```bash
curl -X 'POST' \
  'http://localhost:8080/api/clientes' \
  -H 'Content-Type: application/json' \
  -d '{
  "nome": "Maria Tech Lead",
  "cpf": "123.456.789-00",
  "email": "maria@tech.com",
  "telefone": "11999999999"
}'
```
> 📝 **Anote o ID** retornado (ex: `uuid-cliente`).

### Passo 3: Realizar Venda (Status: Aguardando Pagamento)
Substitua os IDs abaixo pelos valores retornados nos passos anteriores.

```bash
curl -X 'POST' \
  'http://localhost:8080/api/vendas' \
  -H 'Content-Type: application/json' \
  -d '{
  "veiculoId": "COLE_O_ID_DO_VEICULO_AQUI",
  "clienteId": "COLE_O_ID_DO_CLIENTE_AQUI"
}'
```
> 📝 **Anote o `codigoTransacao`** retornado na resposta JSON. Você precisará dele para o webhook.

### Passo 4: Processar Pagamento (Webhook)
Simula o gateway de pagamento confirmando a transação. O status deve ser `"Aprovado"` ou `"Recusado"`.

```bash
curl -X 'POST' \
  'http://localhost:8080/api/pagamentos/webhook' \
  -H 'Content-Type: application/json' \
  -d '{
  "codigoTransacao": "COLE_O_CODIGO_TRANSACAO_AQUI",
  "status": "Aprovado"
}'
```
✅ **Resultado:** O veículo agora terá o status `Vendido` e não poderá ser editado.

---

## 💓 Monitoramento

O Kubernetes utiliza estes endpoints para verificar a saúde do Pod:

| Endpoint | Descrição |
| :--- | :--- |
| `GET /health` | Retorna **200 OK** ("Healthy") se a API estiver rodando. |

---
**Desenvolvido para o Tech Challenge - Fase 2**
