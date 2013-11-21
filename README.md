Brainfuck: Engage your cerebrum in coitus.
=========

A Brainfuck Suite (to be). To date, only an Interpreter exists.

A word on usage:
---------
It was my intention to build the interpreter as generic as possible. It uses events for in- and output,
it can process instructions one at a time (caching those needed for looping), and is built against an interface
that allows to build decorator classes. Performance, however, was not my declared goal.
