# GreedFlameTale-RPG

**Bootcamp GFT Start 4 .NET - Digital Innovation One**

Digital Innovation One - Abstraindo um Jogo de RPG Usando Orientação a Objetos com C# :video_game:

## Mecânica
O objetivo do jogo é escolher um personagem e derrotar outros. Mas cuidado ao escolher, pois cada classe possui vantagens e desvantagens.

## Classes de personagem
A classe do personagem não é só uma palavra legal pra acompanhar o nome. A classe define várias coisas:
- Com quanto de cada atributo ele começa;
- Quanto de cada atributo ele ganha quando sobe de nível;
- Como ele ataca outro personagem;
- Como é o ataque especial;

**SEÇÃO EM CONSTRUÇÃO**

## Atributos comuns
Nesse jogo, todos personagem possuem esses atributos:
- **HP**: Quanta porrada o personagem aguenta. Se o **HP** de um personagem chega a zero, ele é derrotado;
- **MP**: Se a magia dele dói;
- **ATK**: Mostra se as porrada dói ou não;
- **STA**: Conhecido como Energia, Mana, Fôlego, Gás. Mostra se o personagem pode fazer mais alguma coisa;
- **DEF**: Armadura ou defesa. Pro ataque inimigo doer menos.
- **REG**: Poder de regeneração. O quanto se recupera se não fazer nada.

## Ações do personagem
O personagem pode:
- **Atacar**: Cada classe de personagem ataca de um jeito diferente. Mas todos gastam um pouco de **STA** pra soltar um ataque. Todo ataque requer um mínimo de **STA**.
- Dar um **Ataque Especial**: Cada personagem possui um ataque especial, que é mais vantajoso, mais tem um custo maior, como gastar mais **STA**. Também requer um mínimo de **STA** pra ser usado; 
- **Descansar**: Todo mundo pode. Mas aqui, fazer nada regenera um pouco de **HP**. Pode ser usado quando não se tem **STA** disponível pra outra ação;
- **Curar**: Recupera um pouco de **HP**.
- **Aumentar Nível**: Depois de vencer um inimigo com nível maior ou igual, o nível é aumentado. Aumenta todos os atributos, mas o aumento varia de acordo com a classe do personagem.
