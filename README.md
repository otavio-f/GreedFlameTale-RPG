# GreedFlameTale-RPG

**Bootcamp GFT Start 4 .NET - Digital Innovation One**

**README EM CONSTRUÇÃO**

Digital Innovation One - Abstraindo um Jogo de RPG Usando Orientação a Objetos com C# :video_game:

## Mecânica
O objetivo do jogo é escolher um personagem e derrotar outros. Mas cuidado ao escolher, pois cada classe possui vantagens e desvantagens.

## Classes
A classe do personagem não é só uma palavra legal pra acompanhar o nome. A classe define várias coisas:
- Com quanto de cada atributo ele começa;
- O que pode fazer em questão de habilidade;
- Quanto de cada atributo ele ganha quando sobe de nível;
- Como ele ataca outro personagem;
- Como é o ataque especial;

**SEÇÃO EM CONSTRUÇÃO**

## Atributos
Nesse jogo, todos os personagens possuem (pelo menos) esses atributos:
- **HP**: Quanta porrada o personagem aguenta. Se o **HP** de um personagem chega a zero, ele é derrotado;
- **MP**: Se a magia é forte, quanto maior, mais dano;
- **ATK**: Se tem força, quanto maior, mais as porradas doem;
- **STA**: Conhecido como Energia, Mana, Fôlego, Gás, etc. Mostra se o personagem pode fazer mais ou menos coisas;
- **DEF**: Pro ataque inimigo doer menos.
- **REG**: Poder de regeneração. O quanto se recupera se descansar ou curar.

## Ações do personagem
O personagem pode:
- **Atacar**: Cada classe de personagem ataca de um jeito diferente, mas todos gastam um pouco de **STA** pra soltar um ataque. Por isso pra atacar tem que ter pelo menos a **STA** que vai ser gasto.
- Dar um **Ataque Especial**: Cada personagem possui um ataque que tem um custo maior e é mais vantajoso. Também requer um mínimo de **STA** pra ser usado; 
- **Descansar**: Todo mundo pode. Mas aqui, fazer nada regenera um pouco de **STA**. Pode ser usado quando não se tem **STA** disponível pra outra ação;
- **Curar**: Recupera um pouco de **HP**.
- **Aumentar Nível**: Depois de vencer um inimigo com nível maior ou igual, o nível é aumentado. Aumenta todos os atributos, mas o aumento varia de acordo com a classe do personagem.
