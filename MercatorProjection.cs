using System;

public static class MercatorProjection
{
    private static readonly double R_MAJOR = 6378137.0;
    private static readonly double R_MINOR = 6356752.3142;
    private static readonly double RATIO = R_MINOR / R_MAJOR;
    private static readonly double ECCENT = Math.Sqrt(1.0 - (RATIO * RATIO));
    private static readonly double COM = 0.5 * ECCENT;
    private static readonly double DEG2RAD = Math.PI / 180.0;
    private static readonly double RAD2Deg = 180.0 / Math.PI;
    private static readonly double PI_2 = Math.PI / 2.0;

    /// <summary>
    /// lat, lon to pixel as an array
    /// </summary>
    /// <param name="lon">longitude</param>
    /// <param name="lat">latitude</param>
    /// <returns>lon, lat</returns>
    public static double[] toPixel(double lon, double lat)
    {
        return new double[] { lonToX(lon), latToY(lat) };
    }

    /// <summary>
    /// x,y(z) to lat and lon in an array
    /// </summary>
    /// <param name="x">x</param>
    /// <param name="y">z</param>
    /// <returns>x,z</returns>
    public static double[] toGeoCoord(double x, double y)
    {
        return new double[] { xToLon(x), yToLat(y) };
    }

    /// <summary>
    /// longitude to x 
    /// </summary>
    /// <param name="lon">longitude</param>
    /// <returns>x</returns>
    public static double lonToX(double lon)
    {
        return R_MAJOR * DegToRad(lon);
    }

    /// <summary>
    /// latitude to z 
    /// </summary>
    /// <param name="lat">latitude</param>
    /// <returns>z</returns>
    public static double latToY(double lat)
    {
        lat = Math.Min(89.5, Math.Max(lat, -89.5));
        double phi = DegToRad(lat);
        double sinphi = Math.Sin(phi);
        double con = ECCENT * sinphi;
        con = Math.Pow(((1.0 - con) / (1.0 + con)), COM);
        double ts = Math.Tan(0.5 * ((Math.PI * 0.5) - phi)) / con;
        return 0 - R_MAJOR * Math.Log(ts);
    }

    /// <summary>
    /// x to longitude
    /// </summary>
    /// <param name="x">x</param>
    /// <returns>lon</returns>
    public static double xToLon(double x)
    {
        return RadToDeg(x) / R_MAJOR;
    }

    /// <summary>
    /// z to latitude
    /// </summary>
    /// <param name="y">z</param>
    /// <returns>lat</returns>
    public static double yToLat(double y)
    {
        double ts = Math.Exp(-y / R_MAJOR);
        double phi = PI_2 - 2 * Math.Atan(ts);
        double dphi = 1.0;
        int i = 0;
        while ((Math.Abs(dphi) > 0.000000001) && (i < 15))
        {
            double con = ECCENT * Math.Sin(phi);
            dphi = PI_2 - 2 * Math.Atan(ts * Math.Pow((1.0 - con) / (1.0 + con), COM)) - phi;
            phi += dphi;
            i++;
        }
        return RadToDeg(phi);
    }

    /// <summary>
    /// radian to degree
    /// </summary>
    /// <param name="rad">radian</param>
    /// <returns>degree</returns>
    private static double RadToDeg(double rad)
    {
        return rad * RAD2Deg;
    }

    /// <summary>
    /// degree to radian
    /// </summary>
    /// <param name="deg">degree</param>
    /// <returns>radian</returns>
    private static double DegToRad(double deg)
    {
        return deg * DEG2RAD;
    }
}
