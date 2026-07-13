# Shooter 3D estilo Doom

> **Importante:** el informe del proyecto y el enlace al video de demostracion se encuentran en [`INFORME_PROYECTO.md`](INFORME_PROYECTO.md).

Proyecto desarrollado en Unity como ampliacion de un shooter 3D estilo Doom. El objetivo fue implementar sistemas principales de juego: movimiento en primera persona, disparo, municion, recarga, enemigos con IA, contador de enemigos, condicion de victoria, Game Over y feedback visual de dano.

## Contenido principal

- `Assets/Scripts/PrimeraPersona.cs`: movimiento del jugador y camara en primera persona.
- `Assets/Scripts/Disparar.cs`: disparo, municion, recarga y raycast.
- `Assets/Scripts/Vida.cs`: sistema de vida comun para jugador y enemigos.
- `Assets/Scripts/UIMunicion.cs`: texto de municion en pantalla.
- `Assets/Scripts/EnemigoPerseguidor.cs`: persecucion del jugador con NavMesh.
- `Assets/Scripts/EnemigoDisparador.cs`: ataque enemigo por raycast.
- `Assets/Scripts/ControladorNivel.cs`: contador de enemigos y condicion de victoria.
- `Assets/Scripts/GameOverManager.cs`: pantalla de Game Over y reinicio.
- `Assets/Scripts/FeedbackDanoUI.cs`: feedback visual rojo al recibir dano.

## Documentacion

- [`INFORME_PROYECTO.md`](INFORME_PROYECTO.md): informe breve de decisiones tecnicas y enlace al video.

## Version

Proyecto creado con Unity `6000.3.19f1`.
