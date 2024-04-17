# MonoLog
From other devices & files to websites & clouds, genuinely need to be able to log to several.

Also prefer to do so via a binary executable.

Adding configurable logging, therefore, my choices were either to (1) update the EzLog proper, or to (2) simply re-write EzLog using C#. 

The later was faster. Also allows using Mono for any GUI if, as well as when.

One interesting feature here was to use '[deletages](https://github.com/soft9000/MonoLog/blob/aac4e9d004b65b6bebb598e6f3ba537ebecec3a7/MonoLog01/ConMain.cs#L22)' (function pointers) to manage an extensible, whole-line argument processing strategy. While passing around pointers to parameterized functions is a relatively advanced concept in C/C++, the idiom is easier to 'grok in C#. Delegates are also a LOT more powerful for use in parameter-parsing function-factories.

Also mundane for some yet possibly new to several is the 3-state '[troolean](https://github.com/soft9000/MonoLog/blob/aac4e9d004b65b6bebb598e6f3ba537ebecec3a7/MonoLog01/TROOL.cs#L12)' idea. -Don't code add-ons, without it?

p.s: `MonoLog.exe` is just too error prone on POSIX, so we're using `mlog` as the 'official.
