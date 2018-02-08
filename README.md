# RepTeam8

## Your guide to this repository

This repository contains three projects, *so far*. The RSS_LogicEngine project will contain all of our application's logic, while the RSS_UI project will implement our user interface.

The logic engine project has a corresponding test project. I highly recommend **-before you write functional code-** to first write unit tests for it. This serves two purposes.

The first purpose it so that you can write the unit test how you want the code to be used (how you want the code to read). This way, when you get to writing the code, you are can find the solution that fits the unit tests, and thus, find the solution that allows the code to be used in the simplest/most convenient way.

The second purpose for writing unit tests before hand is that you will be writing code to make the unit tests pass, not writing unit tests to pass with the code you just wrote. Do these two statements seem the same? They sholdn't.

## Branches on this repository

This repository should contain a master branch and a staging branch. The staging branch will contain code that is currently in development. The master branch, however, should only contain well tested and finalized code. You will be merging into the staging branch far more often than into the master branch. Usually, a merge of staging into the master branch will happen only when we all agree to do so. Everyone who is part of this project may feel free to create as many branches off of the staging branch as they would like. I will give everyone their own development branch. If, however, you want to work on an additional feature, that you don't currently want in your development branch, you can create another branch for it. Just try to pull from staging often so your branches don't get far behind (resulting in merge conflicts).
