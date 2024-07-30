Modules.GameManager

the lightweight open Framework for Dependency Injection and for controling of Phase (State) execution of Game (like as GameManager):

The GameObject [MODULES] contains script ModulesInstaller which contains links on Game Objects with script derived from GameModule.

Each of these scripts (classes) responsible for installing corresponding “Service” and “Listener”.
It’s a class which contains the list of declaration and initialization of corresponding variables of classes, with special attributes (from namespace Modules.GameManager):
* [Service] – variables (“class”) marked with this attribute can be injected to any classes (by GameModule).
* [Listener] – variables (“class”) marked with this attribute must implement the one from interfaces derived from IGameListener (like as IGamePauseListener) and will be used to callback corresponding methods by GameManager later (at correspoding Game Phase).
Variables must be initialized by empty constructor (or by static value) or be [SerializeField] and set value by Inspector (in case use the MonoBehaviour classes).

The ModulesInstaller (on Awake ()) for each “GameModule” (by use Reflection):
* get the list of “Services” and put them to ServiceLocator of “GameManager”.
* get the list of “Listeners” and put them to different collection in the “GameMachine”.
* make the ResolveDependencies for each variable(classes) which was declared in Module (injection the “services” to coresponding classes) by using the ServiceLocator of “GameManager”. 

The injection will do to all classes which instance was declared in “Modules” scripts (also for variables w/o any special attributes like [Service]/[Listener]).
In these calsses will be called methods marked by [InjectAttribute]attribute (from namespace Modules.GameManager), these methods commonly named as “Constructor (…)”.
The coresponding parameters of these “Constructor (…)” methods will be filled by values from ServiceLocator. (like the “Methods Injection” did in VContainer and Zenject).

The GameManager (like GRASP Facade) will call coresponding methods from GameMachine, which are responsible for coresponding callbacks (like InitGame, Start/Stop/Pause, and Update, FixUpdate and LateUpdate also) of methods from classes which instance was marked in “Modules” like “Listener”. To give possibility to controle the Game execution from one place

GameObject with script “GameManager” give a possibility to manually control of Phase (State) execution of Game and showed the current active state, and also give a possibility to autorun the Game execution.
