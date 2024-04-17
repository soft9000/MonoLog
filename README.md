# MonoLog
From other devices & files to websites & clouds, genuinely need to be able to log to several.

Also prefer to do so via a binary executable.

Adding configurable logging, therefore, my choices were either to (1) update the EzLog proper, or to (2) simply re-write EzLog using C#. 

The later was simply less disrutive to my existing [.ezlog user base](https://github.com/soft9000/era). --Also allows using Mono for any GUI if, as well as when.

## Usage Hightlights
* Configurations managed by [unique, single alphanumeric names](https://github.com/soft9000/MonoLog/blob/0894d2c076e77340a7943e9d3a030a1baeb98f56/MonoLog01/LogConfigDlg.cs#L21).
* Configurations reside [next to the active assembly](https://github.com/soft9000/MonoLog/blob/0894d2c076e77340a7943e9d3a030a1baeb98f56/MonoLog01/LogHome.cs#L15).
* '[TagLines](https://github.com/soft9000/MonoLog/blob/0894d2c076e77340a7943e9d3a030a1baeb98f56/MonoLog01/TagLines.cs#L17)' replace INI Files.
* User [dialog encapsulations](https://github.com/soft9000/MonoLog/blob/0894d2c076e77340a7943e9d3a030a1baeb98f56/MonoLog01/LogConfigDlg.cs#L13) permit future TUIGUI (read `mono`) 'Ops.

## Testing Highlights
* The [dialogs were written to be testable](https://github.com/soft9000/MonoLog/blob/49b4adafd89b919f57f10880d3388e50869cbd85/MonoLog01/MonoTest/test/LogConfigDlgTest.cs#L24).
* The [main CLI was written to be tested](https://github.com/soft9000/MonoLog/blob/49b4adafd89b919f57f10880d3388e50869cbd85/MonoLog01/MonoTest/test/ConMainTest.cs#L24).

## Code Highlights
One interesting feature here was to use '[deletages](https://github.com/soft9000/MonoLog/blob/aac4e9d004b65b6bebb598e6f3ba537ebecec3a7/MonoLog01/ConMain.cs#L22)' (function pointers) to manage an extensible, whole-line argument processing strategy. While passing around pointers to parameterized functions is a relatively advanced concept in C/C++, the idiom is easier to 'grok in C#. Delegates are also a LOT more powerful for use in parameter-parsing function-factories.

Also mundane for some yet possibly new to several is the 3-state '[troolean](https://github.com/soft9000/MonoLog/blob/aac4e9d004b65b6bebb598e6f3ba537ebecec3a7/MonoLog01/TROOL.cs#L12)' idea. -Don't code add-ons, without it?

## Backstory
None too bad for a tad 'ore a day's coding, methinks - yet this 'be _at least_ the _5th time_ that I've crafted the concept _in just as many_ programming languages (C, C++, Java, Python now C#.)

p.s: Typing `MonoLog` is just too error prone on POSIX, so we're using `mlog` as the 'official.

## zSupport?
If you want to support the effort, I seek no donations. Instead, simply feel free to purchase one of [my educational](https://www.udemy.com/user/randallnagy2/) or [printed](https://www.amazon.com/Randall-Nagy/e/B08ZJLH1VN?ref=sr_ntt_srch_lnk_1&qid=1660050704&sr=8-1) productions?
