# World Mercator Projection for Unity's Terrain

This tool functions as a world Mercator coordinate calculator using the EPSG:3395 projection. It efficiently takes latitude and longitude inputs and seamlessly converts them into world Mercator coordinates, facilitating bidirectional conversion between the two coordinate systems.

EPSG:3395 referene: https://epsg.io/3395


# How to Use:

It Convert Latitude and longitude to X and Y coordinates in world Mercator
```c#
double x = MercatorProjection.lonToX(lon);
double y = MercatorProjection.latToY(lat);
```
It Converts x and y to latitude and longitude.
```c#
double lat = MercatorProjection.yToLat(y)
double lon = MercatorProjection.XToLon(x)
```
