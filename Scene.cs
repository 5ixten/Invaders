using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class Scene
{
     private List<Entity> _entities;
     private List<Entity> _queuedSpawns;
     
     public AssetManager AssetManager;
     public SoundManager SoundManager;
     public EventManager EventManager;
     public InputManager InputManager;

     public readonly int BaseEnemySpawnSpeed = 3000;
     public int EnemySpawnSpeed;
     public float Score;
     
     public SceneState SceneState { get; private set; }
     public bool JustLoadedState { get; private set; }
     public readonly Clock Clock;

     public Scene()
     {
          SceneState = SceneState.MAIN_MENU;
          
          _queuedSpawns = new List<Entity>();
          _entities = new List<Entity>();
          Clock = new Clock();
          JustLoadedState = true;

          EnemySpawnSpeed = BaseEnemySpawnSpeed;
          
          AssetManager = new AssetManager();
          SoundManager = new SoundManager();
          EventManager = new EventManager();
          InputManager = new InputManager();
          
          Spawn(new MainMenuGUI());
          Spawn(new HighScoreMenuGUI());
          Spawn(new InGameGUI());
          Spawn(new GameOverGUI());
          Spawn(new QuitGUI());
     }

     public void UpdateAll(float deltaTime)
     {
          SceneState lastState = SceneState;
          
          // Kill all game related entities
          if (JustLoadedState && SceneState == SceneState.GAME_OVER)
          {
               foreach (Entity entity in _entities)
               {
                    entity.IsDead = !entity.DontDestroyOnLoad;
               }
          }
          
          foreach (Entity entity in _queuedSpawns)
          {
               Spawn(entity);
          }
          
          _queuedSpawns.Clear();
          
          foreach (Entity entity in _entities)
          {
               entity.Update(this, deltaTime);
          }

          _entities.RemoveAll(e =>
          {
               if (e.IsDead)
               {
                    e.Destroy(this);
                    return true;
               }
               return false;
          });

          InputManager.Update();
          JustLoadedState = lastState != SceneState;
     }

     public void RenderAll(RenderTarget target)
     {
          foreach (Entity entity in _entities)
          {
               entity.Render(target);
          }
     }

     public void QueueSpawn(Entity entity)
     {
          _queuedSpawns.Add(entity);
     }

     public void Spawn(Entity entity)
     {
          entity.Create(this);
          
          int index = _entities.FindIndex(e => e.ZIndex >= entity.ZIndex);
          
          if (index < 0) _entities.Add(entity); // if index is at start
          else _entities.Insert(index, entity); // if found index
     }

     public void SetSceneState(SceneState state)
     {
          SceneState = state;
     }
     
     public bool FindByType<T>(out T found) where T : Entity 
     {
          foreach (var entity in _entities)
          {
               if (!entity.IsDead && entity is T typed) {
                    found = typed;
                    return true;
               }
          }
        
          found = default(T);
          return false;
     }

     public IEnumerable<Entity> FindIntersects(FloatRect bounds)
     {
          int lastEntity = _entities.Count - 1;
        
          for (int i = lastEntity; i >= 0; i--) 
          {
               Entity entity = _entities[i];
               if (entity.IsDead) continue;
               if (entity.Bounds.Intersects(bounds)) 
               {
                    yield return entity;
               }
          }
     }
}
