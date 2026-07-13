# ENLACE AL VIDEO: 
https://drive.google.com/file/d/12KVJeaOZ6q4N9pD04-hA8udkggSxj0y_/view?usp=sharing

# Informe del proyecto: Shooter 3D estilo Doom

Se mantuvo la base original de movimiento, camara y disparo, y se agregaron los sistemas necesarios para cumplir el parcial: municion, recarga, enemigos con IA, contador de enemigos, condicion de victoria, Game Over y feedback visual de daño.

## Enfoque general

La decision principal fue mantener una arquitectura simple y facil de entender. Cada script tiene una responsabilidad concreta, lo que permite probar y corregir cada sistema por separado.

`PrimeraPersona.cs` se mantiene enfocado en el movimiento y la camara del jugador. `Vida.cs` funciona como sistema comun para jugador y enemigos, permitiendo recibir daño, morir y avisar a otros scripts mediante eventos. Esto evita duplicar logica y hace que sistemas como la UI, el Game Over o el contador de enemigos puedan reaccionar sin modificar directamente la vida del personaje.

## Decisiones de implementacion

- `Disparar.cs` se amplio para manejar municion limitada, recarga con la tecla `R` y una espera antes de recuperar las balas.
- `UIMunicion.cs` se encarga de mostrar las balas en pantalla, separando la logica del arma de la interfaz.
- Los enemigos usan `NavMeshAgent`, porque Unity ya ofrece una solucion adecuada para moverse por el escenario sin atravesar paredes.
- La IA enemiga se dividio en `EnemigoPerseguidor.cs` y `EnemigoDisparador.cs`, separando movimiento y ataque.
- `ControladorNivel.cs` centraliza el progreso del nivel: cuenta enemigos, actualiza la UI y verifica la condicion de victoria.
- `MetaNivel.cs` detecta cuando el jugador llega a la salida, pero la victoria solo ocurre si todos los enemigos fueron eliminados.
- `GameOverManager.cs` reemplaza el reinicio automatico por un panel de Game Over con boton `Reintentar`.

## Sistemas principales

El sistema de vida usa eventos para avisar cuando un objeto recibe daño o muere. Esta decision permite conectar otros sistemas sin mezclar responsabilidades. Por ejemplo, el contador de enemigos escucha la muerte de enemigos, y el feedback rojo escucha cuando el jugador recibe dano.

El sistema de enemigos combina navegacion y ataque. Primero el enemigo persigue al jugador usando NavMesh; cuando esta a distancia suficiente, se detiene y ataca mediante raycast. Se eligio raycast porque es coherente con el disparo del jugador y funciona bien para un shooter estilo Doom.

La condicion de victoria fue disenada segun el enunciado: no basta con llegar a la meta. El jugador debe eliminar a todos los enemigos y luego entrar en la zona de salida. Esto obliga a completar el objetivo del nivel antes de terminar la partida.

## Resultado

El resultado es un shooter funcional con movimiento en primera persona, disparo, municion, recarga, enemigos que persiguen y atacan, contador de enemigos, victoria condicionada, Game Over con reinicio y feedback visual de dano.

Se priorizo cumplir los requisitos usando herramientas propias de Unity como `Canvas`, `Text`, `Button`, `AudioSource`, `Raycast` y `NavMesh`, evitando complejidad innecesaria. El proyecto queda organizado en sistemas pequenos, entendibles y faciles de ampliar.
