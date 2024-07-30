Repository use Git LFS

Unity 2022.2.17

The Unity project in folder “.\2DDefender

Scene folder “.\2DDefender\Assets\Game\_Scenes”
- Defender.unity (default aspect 720:1280)

Specific Stack (Plugin):
- Zenject
- ODIN
- DOTween
- TextMesh Pro
- Asyncoroutine (in Modules)

Description:
Simple demo ShootEmUp prototype game developed based on OOD with SOLID and GRASP principles and use Dependency Injection.

For Dependency Injection and for controling the Phase (State) execution of Game (like as GameManager) was used the lightweight open Framework ”GameManager” (Modules.GameManager).
It uses the correposding child GameObjects “Modules” in [MODULES] GameObject for injection dependencies. [see description of Framework in ReadMe_DI_GameManager.txt]
To activate debugging of dependency injection use the "DEBUG_MODULES" conditional compilation symbol.

Additionally, was used the Framework Zeject (GameObject [SCENE_CONTEXT]) to use the Pool functionallity of Zeject, based on use the IPoolable<IMemoryPool> for managing the Bullets and Enemy objects, but injection of coresponding classed was did by Framework ”GameManager”.

To optimize performance (to decearse number of GameObject which use the FixUpdate() callback) in scirpt EnemyActiveTracker was created the object which contains link to all active enemy throught interface IEnemyFixUpdateListeners. To use this inteface to call the method OnFixedUpdate(float fixedDeltaTime) from these objects on cycle of  MonoBehaviour.FixUpdate().










