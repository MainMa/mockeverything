# MockEverything

This library consists of a tampering mechanism which makes it possible to rewrite methods, getters, setters and constructors of third-party libraries used by the tested code.

Everyone has to deal with untested legacy code which was made with no architecture or design in mind. Such code has no abstractions whatsoever, and heavily depends on specific libraries or parts of the framework. It is impossible to unit test such code without refactoring it first. But, refactoring requires regression testing, so this is a common chicken or the egg problem: you can't test without refactoring, and you can't refactor without tests.

An elementary example would be a method which, somewhere deep in its business logic, makes a call to [`WebClient.DownloadString(Uri) : string`](https://msdn.microsoft.com/en-us/library/ms144200.aspx) method. In order to test it, one needs to rely on the actual web resources, which is not really the spirit of unit tests.

And then, there is SharePoint. If you have seen legacy code of SharePoint web apps, you know how terrible they could be, and how are they impossible to test. When most methods do requests such as `SPContext.Current.Site.Url`, things become particularly hairy when it comes to unit testing.

Unfortunately, as I explained in [a related article](http://blog.pelicandd.com/article/91/tampering-sharepoint-assemblies-part-1), there are currently no Microsoft or third-party products which make it possible to unit test such code. There was Microsoft Fakes coupled with SharePoint.Emulators, but for .NET Framework 3.5 only. Also, the common techniques such as proxying or AOP are unable to solve this problem.

## Design

The system has two parts which are called by a consumer: the discovery and the tampering.

A consumer could be a build task or a simple console application, or a system test. A consumer starts by interacting with the discovery in order to get the pairs which combine the proxy assemblies with the target ones. Then, for every pair, the tampering is called.

The tampering is done in four steps:

 1. ILMerge is called to merge the proxy and the target assemblies.

 1. If needed, the version of the resulting assembly is modified. This is especially useful when the target assembly comes from GAC: with [`bindingRedirect`](https://msdn.microsoft.com/en-us/library/eftw1fys.aspx), the local assembly can be used instead of the one in the GAC.

 1. The resulting assembly public key is altered. This step is necessary when dealing with signed assemblies. Although private key of the target assembly remains unknown, only the public key is needed, since its validation is not done when running code in full trust.

 1. The actual tampering is done, replacing the code within the target methods by the code of the corresponding proxy methods.

To do its job, tampering relies on the [browsers](https://github.com/MainMa/mockeverything/tree/master/MockEverything/Source/Engine/Browsers): one for the types, another one for the methods. Those browsers search the proxy assembly or type for possible proxies, and match them with the corresponding types or methods from the target assembly: the matching is done by [matching classes](https://github.com/MainMa/mockeverything/tree/master/MockEverything/Source/Engine/Matching).

In turn, browsers use the [inspection facade](https://github.com/MainMa/mockeverything/tree/master/MockEverything/Source/Inspection) to get the information they need from the assemblies. Currently, the only inspection facade uses Mono.Cecil. A similar facade can be created for System.Reflection.

The changing of the version and the public key is done directly by the inspection facade. In the same way, the facade handles the replacement of the method body by another.

## Status

The library is currently in development. In can already be used for third-party projects, but lacks documentation and behaves badly when it comes to handling edge cases.

## Limitations

The system contains the following limitations:

 1. `mscorlib.dll` cannot be tampered because of the issue within [ILRepack](https://github.com/gluck/il-repack/issues/53). The [attempt](https://github.com/MainMa/il-repack) was made to modify ILRepack, but I encountered too many problems with the library for now.

 1. [Type parameters covariance](https://msdn.microsoft.com/en-us/library/vstudio/dd469487.aspx) is ignored when doing the matching between proxies and their targets. I've never seen it being used in .NET Framework or third-party libraries; therefore, I assume that it is unnecessary to make the code more difficult for such edge case.

 1. The access to the assemblies is done through .NET Framework's `System.IO` methods, which means the terrible [259-characters paths](http://stackoverflow.com/q/5188527/240613) limitation.

 1. Both proxy and target assemblies should be class libraries. Given that the primary goal of this project is to tamper third-party libraries, this limitation seems fair.

## See also

 - [The original article](http://blog.pelicandd.com/article/91/tampering-sharepoint-assemblies-part-1) where I explain that there are currently no Microsoft or third-party products which make it possible to do what this library does; this includes Microsoft Fakes with SharePoint.Emulators, proxying and AOP.

 - [A working prototype](http://source.pelicandd.com/codebase/tampering/prototypes/TamperingForTests/) was used to study the technical feasability of the approach. It was successfully used on a legacy SharePoint project by making it possible to unit test a few methods. The approach used in the prototype is explained in [an article](http://blog.pelicandd.com/article/92/tampering-sharepoint-assemblies-part-2).

 - [Why do my interceptors not called when using the proxy?](http://stackoverflow.com/q/31826983/240613) question on Stack Overflow showing why Castle DynamicProxy cannot be used as an alternative.

 - [How to do regression testing of code which relies heavily on SP... classes?](http://sharepoint.stackexchange.com/q/151289/40736) question on SharePoint.SE posted to see if there are actual alternatives I have missed.

 - [How do I ignore the assembly reference mismatch in unit tests?](http://stackoverflow.com/q/31900069/240613) question on Stack Overflow related to public key tokens in a context of a signed assembly tampering.

 - [When tampering an assembly, why can't I remove original instructions?](http://stackoverflow.com/q/31969111/240613) question on Stack Overflow I asked when working on the prototype, related to a problem with try/catch blocks within the tampered methods.

## Contributions

Contributions are welcome and can be made through GitHub forks. If you need additional information, please contact me at arseni.mourzenko@gmail.com.