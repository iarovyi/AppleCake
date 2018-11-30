
void OverrideTask(string name, Action action) => ModifyActions(name, action, 0, cleanOtherHandlers: true);
void BeforeTask(string name, Action action) => ModifyActions(name, action, 0, cleanOtherHandlers: false);
void AfterTask(string name, Action action) => ModifyActions(name, action, GetTask(name).Actions.Count, cleanOtherHandlers: false);
void SkipTask(string name) => OverrideTask(name, ()=> Information($"Task '{name}' was skipped"));
Cake.Core.CakeTask GetTask(string name) => (Cake.Core.CakeTask)Tasks.FirstOrDefault(task => task.Name == name);

void ModifyActions(string taskName, Action newAction, int? newActionIndex, bool cleanOtherHandlers){
    Func<ICakeContext, Task> newHandler = (context)=> {
        newAction();
        return System.Threading.Tasks.Task.CompletedTask;
    };

    var task = GetTask(taskName);
    if (cleanOtherHandlers) {
        task.Actions.Clear();
    }
    
    if (newAction != null) {
        task.Actions.Insert((int)newActionIndex, newHandler);
    }
}

//TODO MakeTaskIndependent("");
//TODO TaskShouldDependOn("", "");

//TODO CleanTaskCriterias
//TODO OnTaskFailed

