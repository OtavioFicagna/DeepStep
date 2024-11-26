# Desenvolvido por: Otávio Augusto Ficagna (195749) e Henrique Linck Poerschke (179791)

# DeepStep - Roguelike de Turnos

Bem-vindo ao **DeepStep**, um jogo roguelike de turnos desenvolvido com Unity. Este projeto explora os princípios básicos de jogos roguelike, incluindo geração procedural, e combate estratégico por turnos.

---

## 🎮 Funcionalidades do Jogo

- **Combate por turnos:** Estratégia e planejamento são essenciais para sobreviver.
- **Geração procedural:** Mapas únicos a cada partida.
- **Progressão do personagem:** Colete comida e sobreviva.
- **Estilo roguelike clássico:** Morte permanente e alta rejogabilidade.

---

## 🛠️ Arquitetura do Projeto

O desenvolvimento do DeepStep segue uma abordagem modular baseada em boas práticas de desenvolvimento de jogos Desktop na Unity. 

### Padrões Utilizados
- **Arquitetura ECS (Entity-Component-System):** Separação clara entre dados, lógica e apresentação.
- **Programação Orientada a Eventos:** Para gerenciar interações do jogo de maneira eficiente e desacoplada.
- **Scriptable Objects:** Configuração de dados reutilizáveis, como atributos de inimigos e itens.
- **State Machine:** Gerenciamento de estados para lógica de turnos e comportamento de NPCs.
