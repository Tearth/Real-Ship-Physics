# Real Ship Physics
The prototype of the real ship physics in Unity based on the voxel system. Each ship contains a number of air areas, where every one of them generates a set of voxels. They calculate buoyancy, drag and gravity force (if flood level is greater than zero) and then this force is applied to the ship's rigid body.

This model has a few disadvantages like poor optimization and troubles when Unity has to deal with objects with high speed or small mass (because of physics accuracy). That's why it's only suitable for large ships, which are heavy and relatively slow.

![Example1](https://github.com/Tearth/RealShipPhysics/blob/master/final.gif?raw=true)
![Example2](https://i.imgur.com/wYa4l0m.png)
![Example3](https://i.imgur.com/6H1virX.png)