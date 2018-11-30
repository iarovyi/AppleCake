[![Build status](https://ci.appveyor.com/api/projects/status/0cs93c9c47nh9rql?svg=true)](https://ci.appveyor.com/project/iarovyi/AppleCake)

# Apple Cake
Apple cake is a set of *Yammi* build cake extentions for managing complex setup.
In case when there are many similar repositories than it is handy to have a single standard build cake utilities file with certain predefined tasks.

But the problem comes when referencing script can't modify existing task.
So with following methods it is possible to append/prepend/override/skip existing tasks:

```
#load "./src/utilities.cake"
//#load nuget:?package=AppleCake&include=/**/utilities.cake

Task("DoSomething").Does(() => {

});

OverrideTask("DoSomething", ()=> { 
    Information("Override existing task");
});

BeforeTask("DoSomething", ()=> { 
    Information("Execute something before existing task");
});

AfterTask("DoSomething", ()=> { 
    Information("Execute something after existing task");
});
```

```
Task("AnotherTask").Does(() => {
    
});

SkipTask("AnotherTask");
```

## Usage

Script can be coppied and used via reagular import:
```
#load "./src/utilities.cake"
```

Or it can be place on nuget/myget and than nuget package can be imported:
```
#load nuget:?package=AppleCake&include=/**/utilities.cake
```
