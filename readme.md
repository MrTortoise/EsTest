# Event sourced test framework

## As with all ideas this was given by someone else


In an event sourced system there is a strong pattern in handling a command (or event) in anything.
It is as follows
1. Obtain the history of events
2. Apply those events to get back to current state
3. Handle the command / event and return 0-n events


This means there is a big opportunity for some kind of pattern in test framework.
If this is enforced then the contents of a test that differs between tests becomes data.

This is an eise in attempting to first build a function that can execute all this and then to refactor this into something far cooler.