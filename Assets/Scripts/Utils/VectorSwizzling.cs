/*
 *  Vector Swizzle Extensions by Tyler Glaiel
 *  Version 1.0b- includes swizzles for "0" and "1"
 *  Sample Usage:
        Vector2 a = new Vector2(1, 2);
        Vector4 b = a.yxxy();
        Debug.Log(b); //outputs (2.0, 1.0, 1.0, 2.0)

    0 and 1 swizzling sample usage:
        Vector3 position = new Vector3(20, 10, 5);
        Vector3 flattened_position = position.x0z();
        Debug.log(flattened_position); //outputs (20.0, 0.0, 5.0);
    note: swizzles that start with a 0 or 1 have an underscore in front of them
*/

using UnityEngine;

namespace Utils
{
    //namespace VectorSwizzling {
    internal static class Vector2Swizzles
    {
        //swizzles of size 2
        public static Vector2 _11(this Vector2 a)
        {
            return new Vector2(1.0f, 1.0f);
        }

        public static Vector2 _01(this Vector2 a)
        {
            return new Vector2(0.0f, 1.0f);
        }

        public static Vector2 x1(this Vector2 a)
        {
            return new Vector2(a.x, 1.0f);
        }

        public static Vector2 y1(this Vector2 a)
        {
            return new Vector2(a.y, 1.0f);
        }

        public static Vector2 _10(this Vector2 a)
        {
            return new Vector2(1.0f, 0.0f);
        }

        public static Vector2 _00(this Vector2 a)
        {
            return new Vector2(0.0f, 0.0f);
        }

        public static Vector2 x0(this Vector2 a)
        {
            return new Vector2(a.x, 0.0f);
        }

        public static Vector2 y0(this Vector2 a)
        {
            return new Vector2(a.y, 0.0f);
        }

        public static Vector2 _1x(this Vector2 a)
        {
            return new Vector2(1.0f, a.x);
        }

        public static Vector2 _0x(this Vector2 a)
        {
            return new Vector2(0.0f, a.x);
        }

        public static Vector2 xx(this Vector2 a)
        {
            return new Vector2(a.x, a.x);
        }

        public static Vector2 yx(this Vector2 a)
        {
            return new Vector2(a.y, a.x);
        }

        public static Vector2 _1y(this Vector2 a)
        {
            return new Vector2(1.0f, a.y);
        }

        public static Vector2 _0y(this Vector2 a)
        {
            return new Vector2(0.0f, a.y);
        }

        public static Vector2 xy(this Vector2 a)
        {
            return new Vector2(a.x, a.y);
        }

        public static Vector2 yy(this Vector2 a)
        {
            return new Vector2(a.y, a.y);
        }

        //swizzles of size 3
        public static Vector3 _111(this Vector2 a)
        {
            return new Vector3(1.0f, 1.0f, 1.0f);
        }

        public static Vector3 _011(this Vector2 a)
        {
            return new Vector3(0.0f, 1.0f, 1.0f);
        }

        public static Vector3 x11(this Vector2 a)
        {
            return new Vector3(a.x, 1.0f, 1.0f);
        }

        public static Vector3 y11(this Vector2 a)
        {
            return new Vector3(a.y, 1.0f, 1.0f);
        }

        public static Vector3 _101(this Vector2 a)
        {
            return new Vector3(1.0f, 0.0f, 1.0f);
        }

        public static Vector3 _001(this Vector2 a)
        {
            return new Vector3(0.0f, 0.0f, 1.0f);
        }

        public static Vector3 x01(this Vector2 a)
        {
            return new Vector3(a.x, 0.0f, 1.0f);
        }

        public static Vector3 y01(this Vector2 a)
        {
            return new Vector3(a.y, 0.0f, 1.0f);
        }

        public static Vector3 _1x1(this Vector2 a)
        {
            return new Vector3(1.0f, a.x, 1.0f);
        }

        public static Vector3 _0x1(this Vector2 a)
        {
            return new Vector3(0.0f, a.x, 1.0f);
        }

        public static Vector3 xx1(this Vector2 a)
        {
            return new Vector3(a.x, a.x, 1.0f);
        }

        public static Vector3 yx1(this Vector2 a)
        {
            return new Vector3(a.y, a.x, 1.0f);
        }

        public static Vector3 _1y1(this Vector2 a)
        {
            return new Vector3(1.0f, a.y, 1.0f);
        }

        public static Vector3 _0y1(this Vector2 a)
        {
            return new Vector3(0.0f, a.y, 1.0f);
        }

        public static Vector3 xy1(this Vector2 a)
        {
            return new Vector3(a.x, a.y, 1.0f);
        }

        public static Vector3 yy1(this Vector2 a)
        {
            return new Vector3(a.y, a.y, 1.0f);
        }

        public static Vector3 _110(this Vector2 a)
        {
            return new Vector3(1.0f, 1.0f, 0.0f);
        }

        public static Vector3 _010(this Vector2 a)
        {
            return new Vector3(0.0f, 1.0f, 0.0f);
        }

        public static Vector3 x10(this Vector2 a)
        {
            return new Vector3(a.x, 1.0f, 0.0f);
        }

        public static Vector3 y10(this Vector2 a)
        {
            return new Vector3(a.y, 1.0f, 0.0f);
        }

        public static Vector3 _100(this Vector2 a)
        {
            return new Vector3(1.0f, 0.0f, 0.0f);
        }

        public static Vector3 _000(this Vector2 a)
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }

        public static Vector3 x00(this Vector2 a)
        {
            return new Vector3(a.x, 0.0f, 0.0f);
        }

        public static Vector3 y00(this Vector2 a)
        {
            return new Vector3(a.y, 0.0f, 0.0f);
        }

        public static Vector3 _1x0(this Vector2 a)
        {
            return new Vector3(1.0f, a.x, 0.0f);
        }

        public static Vector3 _0x0(this Vector2 a)
        {
            return new Vector3(0.0f, a.x, 0.0f);
        }

        public static Vector3 xx0(this Vector2 a)
        {
            return new Vector3(a.x, a.x, 0.0f);
        }

        public static Vector3 yx0(this Vector2 a)
        {
            return new Vector3(a.y, a.x, 0.0f);
        }

        public static Vector3 _1y0(this Vector2 a)
        {
            return new Vector3(1.0f, a.y, 0.0f);
        }

        public static Vector3 _0y0(this Vector2 a)
        {
            return new Vector3(0.0f, a.y, 0.0f);
        }

        public static Vector3 xy0(this Vector2 a)
        {
            return new Vector3(a.x, a.y, 0.0f);
        }

        public static Vector3 yy0(this Vector2 a)
        {
            return new Vector3(a.y, a.y, 0.0f);
        }

        public static Vector3 _11x(this Vector2 a)
        {
            return new Vector3(1.0f, 1.0f, a.x);
        }

        public static Vector3 _01x(this Vector2 a)
        {
            return new Vector3(0.0f, 1.0f, a.x);
        }

        public static Vector3 x1x(this Vector2 a)
        {
            return new Vector3(a.x, 1.0f, a.x);
        }

        public static Vector3 y1x(this Vector2 a)
        {
            return new Vector3(a.y, 1.0f, a.x);
        }

        public static Vector3 _10x(this Vector2 a)
        {
            return new Vector3(1.0f, 0.0f, a.x);
        }

        public static Vector3 _00x(this Vector2 a)
        {
            return new Vector3(0.0f, 0.0f, a.x);
        }

        public static Vector3 x0x(this Vector2 a)
        {
            return new Vector3(a.x, 0.0f, a.x);
        }

        public static Vector3 y0x(this Vector2 a)
        {
            return new Vector3(a.y, 0.0f, a.x);
        }

        public static Vector3 _1xx(this Vector2 a)
        {
            return new Vector3(1.0f, a.x, a.x);
        }

        public static Vector3 _0xx(this Vector2 a)
        {
            return new Vector3(0.0f, a.x, a.x);
        }

        public static Vector3 xxx(this Vector2 a)
        {
            return new Vector3(a.x, a.x, a.x);
        }

        public static Vector3 yxx(this Vector2 a)
        {
            return new Vector3(a.y, a.x, a.x);
        }

        public static Vector3 _1yx(this Vector2 a)
        {
            return new Vector3(1.0f, a.y, a.x);
        }

        public static Vector3 _0yx(this Vector2 a)
        {
            return new Vector3(0.0f, a.y, a.x);
        }

        public static Vector3 xyx(this Vector2 a)
        {
            return new Vector3(a.x, a.y, a.x);
        }

        public static Vector3 yyx(this Vector2 a)
        {
            return new Vector3(a.y, a.y, a.x);
        }

        public static Vector3 _11y(this Vector2 a)
        {
            return new Vector3(1.0f, 1.0f, a.y);
        }

        public static Vector3 _01y(this Vector2 a)
        {
            return new Vector3(0.0f, 1.0f, a.y);
        }

        public static Vector3 x1y(this Vector2 a)
        {
            return new Vector3(a.x, 1.0f, a.y);
        }

        public static Vector3 y1y(this Vector2 a)
        {
            return new Vector3(a.y, 1.0f, a.y);
        }

        public static Vector3 _10y(this Vector2 a)
        {
            return new Vector3(1.0f, 0.0f, a.y);
        }

        public static Vector3 _00y(this Vector2 a)
        {
            return new Vector3(0.0f, 0.0f, a.y);
        }

        public static Vector3 x0y(this Vector2 a)
        {
            return new Vector3(a.x, 0.0f, a.y);
        }

        public static Vector3 y0y(this Vector2 a)
        {
            return new Vector3(a.y, 0.0f, a.y);
        }

        public static Vector3 _1xy(this Vector2 a)
        {
            return new Vector3(1.0f, a.x, a.y);
        }

        public static Vector3 _0xy(this Vector2 a)
        {
            return new Vector3(0.0f, a.x, a.y);
        }

        public static Vector3 xxy(this Vector2 a)
        {
            return new Vector3(a.x, a.x, a.y);
        }

        public static Vector3 yxy(this Vector2 a)
        {
            return new Vector3(a.y, a.x, a.y);
        }

        public static Vector3 _1yy(this Vector2 a)
        {
            return new Vector3(1.0f, a.y, a.y);
        }

        public static Vector3 _0yy(this Vector2 a)
        {
            return new Vector3(0.0f, a.y, a.y);
        }

        public static Vector3 xyy(this Vector2 a)
        {
            return new Vector3(a.x, a.y, a.y);
        }

        public static Vector3 yyy(this Vector2 a)
        {
            return new Vector3(a.y, a.y, a.y);
        }

        //swizzles of size 4
        public static Vector4 _1111(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 _0111(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 x111(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 y111(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 _1011(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 _0011(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 x011(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 y011(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 _1x11(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, 1.0f);
        }

        public static Vector4 _0x11(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, 1.0f);
        }

        public static Vector4 xx11(this Vector2 a)
        {
            return new Vector4(a.x, a.x, 1.0f, 1.0f);
        }

        public static Vector4 yx11(this Vector2 a)
        {
            return new Vector4(a.y, a.x, 1.0f, 1.0f);
        }

        public static Vector4 _1y11(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, 1.0f);
        }

        public static Vector4 _0y11(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, 1.0f);
        }

        public static Vector4 xy11(this Vector2 a)
        {
            return new Vector4(a.x, a.y, 1.0f, 1.0f);
        }

        public static Vector4 yy11(this Vector2 a)
        {
            return new Vector4(a.y, a.y, 1.0f, 1.0f);
        }

        public static Vector4 _1101(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 _0101(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 x101(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 y101(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 _1001(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 _0001(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 x001(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 y001(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 _1x01(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, 1.0f);
        }

        public static Vector4 _0x01(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, 1.0f);
        }

        public static Vector4 xx01(this Vector2 a)
        {
            return new Vector4(a.x, a.x, 0.0f, 1.0f);
        }

        public static Vector4 yx01(this Vector2 a)
        {
            return new Vector4(a.y, a.x, 0.0f, 1.0f);
        }

        public static Vector4 _1y01(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, 1.0f);
        }

        public static Vector4 _0y01(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, 1.0f);
        }

        public static Vector4 xy01(this Vector2 a)
        {
            return new Vector4(a.x, a.y, 0.0f, 1.0f);
        }

        public static Vector4 yy01(this Vector2 a)
        {
            return new Vector4(a.y, a.y, 0.0f, 1.0f);
        }

        public static Vector4 _11x1(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, 1.0f);
        }

        public static Vector4 _01x1(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, 1.0f);
        }

        public static Vector4 x1x1(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, a.x, 1.0f);
        }

        public static Vector4 y1x1(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, a.x, 1.0f);
        }

        public static Vector4 _10x1(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, 1.0f);
        }

        public static Vector4 _00x1(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, 1.0f);
        }

        public static Vector4 x0x1(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, a.x, 1.0f);
        }

        public static Vector4 y0x1(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, a.x, 1.0f);
        }

        public static Vector4 _1xx1(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, a.x, 1.0f);
        }

        public static Vector4 _0xx1(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, a.x, 1.0f);
        }

        public static Vector4 xxx1(this Vector2 a)
        {
            return new Vector4(a.x, a.x, a.x, 1.0f);
        }

        public static Vector4 yxx1(this Vector2 a)
        {
            return new Vector4(a.y, a.x, a.x, 1.0f);
        }

        public static Vector4 _1yx1(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, a.x, 1.0f);
        }

        public static Vector4 _0yx1(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, a.x, 1.0f);
        }

        public static Vector4 xyx1(this Vector2 a)
        {
            return new Vector4(a.x, a.y, a.x, 1.0f);
        }

        public static Vector4 yyx1(this Vector2 a)
        {
            return new Vector4(a.y, a.y, a.x, 1.0f);
        }

        public static Vector4 _11y1(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, 1.0f);
        }

        public static Vector4 _01y1(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, 1.0f);
        }

        public static Vector4 x1y1(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, a.y, 1.0f);
        }

        public static Vector4 y1y1(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, a.y, 1.0f);
        }

        public static Vector4 _10y1(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, 1.0f);
        }

        public static Vector4 _00y1(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, 1.0f);
        }

        public static Vector4 x0y1(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, a.y, 1.0f);
        }

        public static Vector4 y0y1(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, a.y, 1.0f);
        }

        public static Vector4 _1xy1(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, a.y, 1.0f);
        }

        public static Vector4 _0xy1(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, a.y, 1.0f);
        }

        public static Vector4 xxy1(this Vector2 a)
        {
            return new Vector4(a.x, a.x, a.y, 1.0f);
        }

        public static Vector4 yxy1(this Vector2 a)
        {
            return new Vector4(a.y, a.x, a.y, 1.0f);
        }

        public static Vector4 _1yy1(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, a.y, 1.0f);
        }

        public static Vector4 _0yy1(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, a.y, 1.0f);
        }

        public static Vector4 xyy1(this Vector2 a)
        {
            return new Vector4(a.x, a.y, a.y, 1.0f);
        }

        public static Vector4 yyy1(this Vector2 a)
        {
            return new Vector4(a.y, a.y, a.y, 1.0f);
        }

        public static Vector4 _1110(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 _0110(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 x110(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 y110(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 _1010(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 _0010(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 x010(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 y010(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 _1x10(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, 0.0f);
        }

        public static Vector4 _0x10(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, 0.0f);
        }

        public static Vector4 xx10(this Vector2 a)
        {
            return new Vector4(a.x, a.x, 1.0f, 0.0f);
        }

        public static Vector4 yx10(this Vector2 a)
        {
            return new Vector4(a.y, a.x, 1.0f, 0.0f);
        }

        public static Vector4 _1y10(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, 0.0f);
        }

        public static Vector4 _0y10(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, 0.0f);
        }

        public static Vector4 xy10(this Vector2 a)
        {
            return new Vector4(a.x, a.y, 1.0f, 0.0f);
        }

        public static Vector4 yy10(this Vector2 a)
        {
            return new Vector4(a.y, a.y, 1.0f, 0.0f);
        }

        public static Vector4 _1100(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 _0100(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 x100(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 y100(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 _1000(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 _0000(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 x000(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 y000(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 _1x00(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, 0.0f);
        }

        public static Vector4 _0x00(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, 0.0f);
        }

        public static Vector4 xx00(this Vector2 a)
        {
            return new Vector4(a.x, a.x, 0.0f, 0.0f);
        }

        public static Vector4 yx00(this Vector2 a)
        {
            return new Vector4(a.y, a.x, 0.0f, 0.0f);
        }

        public static Vector4 _1y00(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, 0.0f);
        }

        public static Vector4 _0y00(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, 0.0f);
        }

        public static Vector4 xy00(this Vector2 a)
        {
            return new Vector4(a.x, a.y, 0.0f, 0.0f);
        }

        public static Vector4 yy00(this Vector2 a)
        {
            return new Vector4(a.y, a.y, 0.0f, 0.0f);
        }

        public static Vector4 _11x0(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, 0.0f);
        }

        public static Vector4 _01x0(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, 0.0f);
        }

        public static Vector4 x1x0(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, a.x, 0.0f);
        }

        public static Vector4 y1x0(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, a.x, 0.0f);
        }

        public static Vector4 _10x0(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, 0.0f);
        }

        public static Vector4 _00x0(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, 0.0f);
        }

        public static Vector4 x0x0(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, a.x, 0.0f);
        }

        public static Vector4 y0x0(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, a.x, 0.0f);
        }

        public static Vector4 _1xx0(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, a.x, 0.0f);
        }

        public static Vector4 _0xx0(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, a.x, 0.0f);
        }

        public static Vector4 xxx0(this Vector2 a)
        {
            return new Vector4(a.x, a.x, a.x, 0.0f);
        }

        public static Vector4 yxx0(this Vector2 a)
        {
            return new Vector4(a.y, a.x, a.x, 0.0f);
        }

        public static Vector4 _1yx0(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, a.x, 0.0f);
        }

        public static Vector4 _0yx0(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, a.x, 0.0f);
        }

        public static Vector4 xyx0(this Vector2 a)
        {
            return new Vector4(a.x, a.y, a.x, 0.0f);
        }

        public static Vector4 yyx0(this Vector2 a)
        {
            return new Vector4(a.y, a.y, a.x, 0.0f);
        }

        public static Vector4 _11y0(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, 0.0f);
        }

        public static Vector4 _01y0(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, 0.0f);
        }

        public static Vector4 x1y0(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, a.y, 0.0f);
        }

        public static Vector4 y1y0(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, a.y, 0.0f);
        }

        public static Vector4 _10y0(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, 0.0f);
        }

        public static Vector4 _00y0(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, 0.0f);
        }

        public static Vector4 x0y0(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, a.y, 0.0f);
        }

        public static Vector4 y0y0(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, a.y, 0.0f);
        }

        public static Vector4 _1xy0(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, a.y, 0.0f);
        }

        public static Vector4 _0xy0(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, a.y, 0.0f);
        }

        public static Vector4 xxy0(this Vector2 a)
        {
            return new Vector4(a.x, a.x, a.y, 0.0f);
        }

        public static Vector4 yxy0(this Vector2 a)
        {
            return new Vector4(a.y, a.x, a.y, 0.0f);
        }

        public static Vector4 _1yy0(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, a.y, 0.0f);
        }

        public static Vector4 _0yy0(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, a.y, 0.0f);
        }

        public static Vector4 xyy0(this Vector2 a)
        {
            return new Vector4(a.x, a.y, a.y, 0.0f);
        }

        public static Vector4 yyy0(this Vector2 a)
        {
            return new Vector4(a.y, a.y, a.y, 0.0f);
        }

        public static Vector4 _111x(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, a.x);
        }

        public static Vector4 _011x(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, a.x);
        }

        public static Vector4 x11x(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, a.x);
        }

        public static Vector4 y11x(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, a.x);
        }

        public static Vector4 _101x(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, a.x);
        }

        public static Vector4 _001x(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, a.x);
        }

        public static Vector4 x01x(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, a.x);
        }

        public static Vector4 y01x(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, a.x);
        }

        public static Vector4 _1x1x(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, a.x);
        }

        public static Vector4 _0x1x(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, a.x);
        }

        public static Vector4 xx1x(this Vector2 a)
        {
            return new Vector4(a.x, a.x, 1.0f, a.x);
        }

        public static Vector4 yx1x(this Vector2 a)
        {
            return new Vector4(a.y, a.x, 1.0f, a.x);
        }

        public static Vector4 _1y1x(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, a.x);
        }

        public static Vector4 _0y1x(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, a.x);
        }

        public static Vector4 xy1x(this Vector2 a)
        {
            return new Vector4(a.x, a.y, 1.0f, a.x);
        }

        public static Vector4 yy1x(this Vector2 a)
        {
            return new Vector4(a.y, a.y, 1.0f, a.x);
        }

        public static Vector4 _110x(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, a.x);
        }

        public static Vector4 _010x(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, a.x);
        }

        public static Vector4 x10x(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, a.x);
        }

        public static Vector4 y10x(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, a.x);
        }

        public static Vector4 _100x(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, a.x);
        }

        public static Vector4 _000x(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, a.x);
        }

        public static Vector4 x00x(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, a.x);
        }

        public static Vector4 y00x(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, a.x);
        }

        public static Vector4 _1x0x(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, a.x);
        }

        public static Vector4 _0x0x(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, a.x);
        }

        public static Vector4 xx0x(this Vector2 a)
        {
            return new Vector4(a.x, a.x, 0.0f, a.x);
        }

        public static Vector4 yx0x(this Vector2 a)
        {
            return new Vector4(a.y, a.x, 0.0f, a.x);
        }

        public static Vector4 _1y0x(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, a.x);
        }

        public static Vector4 _0y0x(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, a.x);
        }

        public static Vector4 xy0x(this Vector2 a)
        {
            return new Vector4(a.x, a.y, 0.0f, a.x);
        }

        public static Vector4 yy0x(this Vector2 a)
        {
            return new Vector4(a.y, a.y, 0.0f, a.x);
        }

        public static Vector4 _11xx(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, a.x);
        }

        public static Vector4 _01xx(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, a.x);
        }

        public static Vector4 x1xx(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, a.x, a.x);
        }

        public static Vector4 y1xx(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, a.x, a.x);
        }

        public static Vector4 _10xx(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, a.x);
        }

        public static Vector4 _00xx(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, a.x);
        }

        public static Vector4 x0xx(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, a.x, a.x);
        }

        public static Vector4 y0xx(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, a.x, a.x);
        }

        public static Vector4 _1xxx(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, a.x, a.x);
        }

        public static Vector4 _0xxx(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, a.x, a.x);
        }

        public static Vector4 xxxx(this Vector2 a)
        {
            return new Vector4(a.x, a.x, a.x, a.x);
        }

        public static Vector4 yxxx(this Vector2 a)
        {
            return new Vector4(a.y, a.x, a.x, a.x);
        }

        public static Vector4 _1yxx(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, a.x, a.x);
        }

        public static Vector4 _0yxx(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, a.x, a.x);
        }

        public static Vector4 xyxx(this Vector2 a)
        {
            return new Vector4(a.x, a.y, a.x, a.x);
        }

        public static Vector4 yyxx(this Vector2 a)
        {
            return new Vector4(a.y, a.y, a.x, a.x);
        }

        public static Vector4 _11yx(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, a.x);
        }

        public static Vector4 _01yx(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, a.x);
        }

        public static Vector4 x1yx(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, a.y, a.x);
        }

        public static Vector4 y1yx(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, a.y, a.x);
        }

        public static Vector4 _10yx(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, a.x);
        }

        public static Vector4 _00yx(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, a.x);
        }

        public static Vector4 x0yx(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, a.y, a.x);
        }

        public static Vector4 y0yx(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, a.y, a.x);
        }

        public static Vector4 _1xyx(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, a.y, a.x);
        }

        public static Vector4 _0xyx(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, a.y, a.x);
        }

        public static Vector4 xxyx(this Vector2 a)
        {
            return new Vector4(a.x, a.x, a.y, a.x);
        }

        public static Vector4 yxyx(this Vector2 a)
        {
            return new Vector4(a.y, a.x, a.y, a.x);
        }

        public static Vector4 _1yyx(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, a.y, a.x);
        }

        public static Vector4 _0yyx(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, a.y, a.x);
        }

        public static Vector4 xyyx(this Vector2 a)
        {
            return new Vector4(a.x, a.y, a.y, a.x);
        }

        public static Vector4 yyyx(this Vector2 a)
        {
            return new Vector4(a.y, a.y, a.y, a.x);
        }

        public static Vector4 _111y(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, a.y);
        }

        public static Vector4 _011y(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, a.y);
        }

        public static Vector4 x11y(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, a.y);
        }

        public static Vector4 y11y(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, a.y);
        }

        public static Vector4 _101y(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, a.y);
        }

        public static Vector4 _001y(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, a.y);
        }

        public static Vector4 x01y(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, a.y);
        }

        public static Vector4 y01y(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, a.y);
        }

        public static Vector4 _1x1y(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, a.y);
        }

        public static Vector4 _0x1y(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, a.y);
        }

        public static Vector4 xx1y(this Vector2 a)
        {
            return new Vector4(a.x, a.x, 1.0f, a.y);
        }

        public static Vector4 yx1y(this Vector2 a)
        {
            return new Vector4(a.y, a.x, 1.0f, a.y);
        }

        public static Vector4 _1y1y(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, a.y);
        }

        public static Vector4 _0y1y(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, a.y);
        }

        public static Vector4 xy1y(this Vector2 a)
        {
            return new Vector4(a.x, a.y, 1.0f, a.y);
        }

        public static Vector4 yy1y(this Vector2 a)
        {
            return new Vector4(a.y, a.y, 1.0f, a.y);
        }

        public static Vector4 _110y(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, a.y);
        }

        public static Vector4 _010y(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, a.y);
        }

        public static Vector4 x10y(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, a.y);
        }

        public static Vector4 y10y(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, a.y);
        }

        public static Vector4 _100y(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, a.y);
        }

        public static Vector4 _000y(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, a.y);
        }

        public static Vector4 x00y(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, a.y);
        }

        public static Vector4 y00y(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, a.y);
        }

        public static Vector4 _1x0y(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, a.y);
        }

        public static Vector4 _0x0y(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, a.y);
        }

        public static Vector4 xx0y(this Vector2 a)
        {
            return new Vector4(a.x, a.x, 0.0f, a.y);
        }

        public static Vector4 yx0y(this Vector2 a)
        {
            return new Vector4(a.y, a.x, 0.0f, a.y);
        }

        public static Vector4 _1y0y(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, a.y);
        }

        public static Vector4 _0y0y(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, a.y);
        }

        public static Vector4 xy0y(this Vector2 a)
        {
            return new Vector4(a.x, a.y, 0.0f, a.y);
        }

        public static Vector4 yy0y(this Vector2 a)
        {
            return new Vector4(a.y, a.y, 0.0f, a.y);
        }

        public static Vector4 _11xy(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, a.y);
        }

        public static Vector4 _01xy(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, a.y);
        }

        public static Vector4 x1xy(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, a.x, a.y);
        }

        public static Vector4 y1xy(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, a.x, a.y);
        }

        public static Vector4 _10xy(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, a.y);
        }

        public static Vector4 _00xy(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, a.y);
        }

        public static Vector4 x0xy(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, a.x, a.y);
        }

        public static Vector4 y0xy(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, a.x, a.y);
        }

        public static Vector4 _1xxy(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, a.x, a.y);
        }

        public static Vector4 _0xxy(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, a.x, a.y);
        }

        public static Vector4 xxxy(this Vector2 a)
        {
            return new Vector4(a.x, a.x, a.x, a.y);
        }

        public static Vector4 yxxy(this Vector2 a)
        {
            return new Vector4(a.y, a.x, a.x, a.y);
        }

        public static Vector4 _1yxy(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, a.x, a.y);
        }

        public static Vector4 _0yxy(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, a.x, a.y);
        }

        public static Vector4 xyxy(this Vector2 a)
        {
            return new Vector4(a.x, a.y, a.x, a.y);
        }

        public static Vector4 yyxy(this Vector2 a)
        {
            return new Vector4(a.y, a.y, a.x, a.y);
        }

        public static Vector4 _11yy(this Vector2 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, a.y);
        }

        public static Vector4 _01yy(this Vector2 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, a.y);
        }

        public static Vector4 x1yy(this Vector2 a)
        {
            return new Vector4(a.x, 1.0f, a.y, a.y);
        }

        public static Vector4 y1yy(this Vector2 a)
        {
            return new Vector4(a.y, 1.0f, a.y, a.y);
        }

        public static Vector4 _10yy(this Vector2 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, a.y);
        }

        public static Vector4 _00yy(this Vector2 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, a.y);
        }

        public static Vector4 x0yy(this Vector2 a)
        {
            return new Vector4(a.x, 0.0f, a.y, a.y);
        }

        public static Vector4 y0yy(this Vector2 a)
        {
            return new Vector4(a.y, 0.0f, a.y, a.y);
        }

        public static Vector4 _1xyy(this Vector2 a)
        {
            return new Vector4(1.0f, a.x, a.y, a.y);
        }

        public static Vector4 _0xyy(this Vector2 a)
        {
            return new Vector4(0.0f, a.x, a.y, a.y);
        }

        public static Vector4 xxyy(this Vector2 a)
        {
            return new Vector4(a.x, a.x, a.y, a.y);
        }

        public static Vector4 yxyy(this Vector2 a)
        {
            return new Vector4(a.y, a.x, a.y, a.y);
        }

        public static Vector4 _1yyy(this Vector2 a)
        {
            return new Vector4(1.0f, a.y, a.y, a.y);
        }

        public static Vector4 _0yyy(this Vector2 a)
        {
            return new Vector4(0.0f, a.y, a.y, a.y);
        }

        public static Vector4 xyyy(this Vector2 a)
        {
            return new Vector4(a.x, a.y, a.y, a.y);
        }

        public static Vector4 yyyy(this Vector2 a)
        {
            return new Vector4(a.y, a.y, a.y, a.y);
        }
    }
    internal static class Vector3Swizzles
    {
        //swizzles of size 2
        public static Vector2 _11(this Vector3 a)
        {
            return new Vector2(1.0f, 1.0f);
        }

        public static Vector2 _01(this Vector3 a)
        {
            return new Vector2(0.0f, 1.0f);
        }

        public static Vector2 x1(this Vector3 a)
        {
            return new Vector2(a.x, 1.0f);
        }

        public static Vector2 y1(this Vector3 a)
        {
            return new Vector2(a.y, 1.0f);
        }

        public static Vector2 z1(this Vector3 a)
        {
            return new Vector2(a.z, 1.0f);
        }

        public static Vector2 _10(this Vector3 a)
        {
            return new Vector2(1.0f, 0.0f);
        }

        public static Vector2 _00(this Vector3 a)
        {
            return new Vector2(0.0f, 0.0f);
        }

        public static Vector2 x0(this Vector3 a)
        {
            return new Vector2(a.x, 0.0f);
        }

        public static Vector2 y0(this Vector3 a)
        {
            return new Vector2(a.y, 0.0f);
        }

        public static Vector2 z0(this Vector3 a)
        {
            return new Vector2(a.z, 0.0f);
        }

        public static Vector2 _1x(this Vector3 a)
        {
            return new Vector2(1.0f, a.x);
        }

        public static Vector2 _0x(this Vector3 a)
        {
            return new Vector2(0.0f, a.x);
        }

        public static Vector2 xx(this Vector3 a)
        {
            return new Vector2(a.x, a.x);
        }

        public static Vector2 yx(this Vector3 a)
        {
            return new Vector2(a.y, a.x);
        }

        public static Vector2 zx(this Vector3 a)
        {
            return new Vector2(a.z, a.x);
        }

        public static Vector2 _1y(this Vector3 a)
        {
            return new Vector2(1.0f, a.y);
        }

        public static Vector2 _0y(this Vector3 a)
        {
            return new Vector2(0.0f, a.y);
        }

        public static Vector2 xy(this Vector3 a)
        {
            return new Vector2(a.x, a.y);
        }

        public static Vector2 yy(this Vector3 a)
        {
            return new Vector2(a.y, a.y);
        }

        public static Vector2 zy(this Vector3 a)
        {
            return new Vector2(a.z, a.y);
        }

        public static Vector2 _1z(this Vector3 a)
        {
            return new Vector2(1.0f, a.z);
        }

        public static Vector2 _0z(this Vector3 a)
        {
            return new Vector2(0.0f, a.z);
        }

        public static Vector2 xz(this Vector3 a)
        {
            return new Vector2(a.x, a.z);
        }

        public static Vector2 yz(this Vector3 a)
        {
            return new Vector2(a.y, a.z);
        }

        public static Vector2 zz(this Vector3 a)
        {
            return new Vector2(a.z, a.z);
        }

        //swizzles of size 3
        public static Vector3 _111(this Vector3 a)
        {
            return new Vector3(1.0f, 1.0f, 1.0f);
        }

        public static Vector3 _011(this Vector3 a)
        {
            return new Vector3(0.0f, 1.0f, 1.0f);
        }

        public static Vector3 x11(this Vector3 a)
        {
            return new Vector3(a.x, 1.0f, 1.0f);
        }

        public static Vector3 y11(this Vector3 a)
        {
            return new Vector3(a.y, 1.0f, 1.0f);
        }

        public static Vector3 z11(this Vector3 a)
        {
            return new Vector3(a.z, 1.0f, 1.0f);
        }

        public static Vector3 _101(this Vector3 a)
        {
            return new Vector3(1.0f, 0.0f, 1.0f);
        }

        public static Vector3 _001(this Vector3 a)
        {
            return new Vector3(0.0f, 0.0f, 1.0f);
        }

        public static Vector3 x01(this Vector3 a)
        {
            return new Vector3(a.x, 0.0f, 1.0f);
        }

        public static Vector3 y01(this Vector3 a)
        {
            return new Vector3(a.y, 0.0f, 1.0f);
        }

        public static Vector3 z01(this Vector3 a)
        {
            return new Vector3(a.z, 0.0f, 1.0f);
        }

        public static Vector3 _1x1(this Vector3 a)
        {
            return new Vector3(1.0f, a.x, 1.0f);
        }

        public static Vector3 _0x1(this Vector3 a)
        {
            return new Vector3(0.0f, a.x, 1.0f);
        }

        public static Vector3 xx1(this Vector3 a)
        {
            return new Vector3(a.x, a.x, 1.0f);
        }

        public static Vector3 yx1(this Vector3 a)
        {
            return new Vector3(a.y, a.x, 1.0f);
        }

        public static Vector3 zx1(this Vector3 a)
        {
            return new Vector3(a.z, a.x, 1.0f);
        }

        public static Vector3 _1y1(this Vector3 a)
        {
            return new Vector3(1.0f, a.y, 1.0f);
        }

        public static Vector3 _0y1(this Vector3 a)
        {
            return new Vector3(0.0f, a.y, 1.0f);
        }

        public static Vector3 xy1(this Vector3 a)
        {
            return new Vector3(a.x, a.y, 1.0f);
        }

        public static Vector3 yy1(this Vector3 a)
        {
            return new Vector3(a.y, a.y, 1.0f);
        }

        public static Vector3 zy1(this Vector3 a)
        {
            return new Vector3(a.z, a.y, 1.0f);
        }

        public static Vector3 _1z1(this Vector3 a)
        {
            return new Vector3(1.0f, a.z, 1.0f);
        }

        public static Vector3 _0z1(this Vector3 a)
        {
            return new Vector3(0.0f, a.z, 1.0f);
        }

        public static Vector3 xz1(this Vector3 a)
        {
            return new Vector3(a.x, a.z, 1.0f);
        }

        public static Vector3 yz1(this Vector3 a)
        {
            return new Vector3(a.y, a.z, 1.0f);
        }

        public static Vector3 zz1(this Vector3 a)
        {
            return new Vector3(a.z, a.z, 1.0f);
        }

        public static Vector3 _110(this Vector3 a)
        {
            return new Vector3(1.0f, 1.0f, 0.0f);
        }

        public static Vector3 _010(this Vector3 a)
        {
            return new Vector3(0.0f, 1.0f, 0.0f);
        }

        public static Vector3 x10(this Vector3 a)
        {
            return new Vector3(a.x, 1.0f, 0.0f);
        }

        public static Vector3 y10(this Vector3 a)
        {
            return new Vector3(a.y, 1.0f, 0.0f);
        }

        public static Vector3 z10(this Vector3 a)
        {
            return new Vector3(a.z, 1.0f, 0.0f);
        }

        public static Vector3 _100(this Vector3 a)
        {
            return new Vector3(1.0f, 0.0f, 0.0f);
        }

        public static Vector3 _000(this Vector3 a)
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }

        public static Vector3 x00(this Vector3 a)
        {
            return new Vector3(a.x, 0.0f, 0.0f);
        }

        public static Vector3 y00(this Vector3 a)
        {
            return new Vector3(a.y, 0.0f, 0.0f);
        }

        public static Vector3 z00(this Vector3 a)
        {
            return new Vector3(a.z, 0.0f, 0.0f);
        }

        public static Vector3 _1x0(this Vector3 a)
        {
            return new Vector3(1.0f, a.x, 0.0f);
        }

        public static Vector3 _0x0(this Vector3 a)
        {
            return new Vector3(0.0f, a.x, 0.0f);
        }

        public static Vector3 xx0(this Vector3 a)
        {
            return new Vector3(a.x, a.x, 0.0f);
        }

        public static Vector3 yx0(this Vector3 a)
        {
            return new Vector3(a.y, a.x, 0.0f);
        }

        public static Vector3 zx0(this Vector3 a)
        {
            return new Vector3(a.z, a.x, 0.0f);
        }

        public static Vector3 _1y0(this Vector3 a)
        {
            return new Vector3(1.0f, a.y, 0.0f);
        }

        public static Vector3 _0y0(this Vector3 a)
        {
            return new Vector3(0.0f, a.y, 0.0f);
        }

        public static Vector3 xy0(this Vector3 a)
        {
            return new Vector3(a.x, a.y, 0.0f);
        }

        public static Vector3 yy0(this Vector3 a)
        {
            return new Vector3(a.y, a.y, 0.0f);
        }

        public static Vector3 zy0(this Vector3 a)
        {
            return new Vector3(a.z, a.y, 0.0f);
        }

        public static Vector3 _1z0(this Vector3 a)
        {
            return new Vector3(1.0f, a.z, 0.0f);
        }

        public static Vector3 _0z0(this Vector3 a)
        {
            return new Vector3(0.0f, a.z, 0.0f);
        }

        public static Vector3 xz0(this Vector3 a)
        {
            return new Vector3(a.x, a.z, 0.0f);
        }

        public static Vector3 yz0(this Vector3 a)
        {
            return new Vector3(a.y, a.z, 0.0f);
        }

        public static Vector3 zz0(this Vector3 a)
        {
            return new Vector3(a.z, a.z, 0.0f);
        }

        public static Vector3 _11x(this Vector3 a)
        {
            return new Vector3(1.0f, 1.0f, a.x);
        }

        public static Vector3 _01x(this Vector3 a)
        {
            return new Vector3(0.0f, 1.0f, a.x);
        }

        public static Vector3 x1x(this Vector3 a)
        {
            return new Vector3(a.x, 1.0f, a.x);
        }

        public static Vector3 y1x(this Vector3 a)
        {
            return new Vector3(a.y, 1.0f, a.x);
        }

        public static Vector3 z1x(this Vector3 a)
        {
            return new Vector3(a.z, 1.0f, a.x);
        }

        public static Vector3 _10x(this Vector3 a)
        {
            return new Vector3(1.0f, 0.0f, a.x);
        }

        public static Vector3 _00x(this Vector3 a)
        {
            return new Vector3(0.0f, 0.0f, a.x);
        }

        public static Vector3 x0x(this Vector3 a)
        {
            return new Vector3(a.x, 0.0f, a.x);
        }

        public static Vector3 y0x(this Vector3 a)
        {
            return new Vector3(a.y, 0.0f, a.x);
        }

        public static Vector3 z0x(this Vector3 a)
        {
            return new Vector3(a.z, 0.0f, a.x);
        }

        public static Vector3 _1xx(this Vector3 a)
        {
            return new Vector3(1.0f, a.x, a.x);
        }

        public static Vector3 _0xx(this Vector3 a)
        {
            return new Vector3(0.0f, a.x, a.x);
        }

        public static Vector3 xxx(this Vector3 a)
        {
            return new Vector3(a.x, a.x, a.x);
        }

        public static Vector3 yxx(this Vector3 a)
        {
            return new Vector3(a.y, a.x, a.x);
        }

        public static Vector3 zxx(this Vector3 a)
        {
            return new Vector3(a.z, a.x, a.x);
        }

        public static Vector3 _1yx(this Vector3 a)
        {
            return new Vector3(1.0f, a.y, a.x);
        }

        public static Vector3 _0yx(this Vector3 a)
        {
            return new Vector3(0.0f, a.y, a.x);
        }

        public static Vector3 xyx(this Vector3 a)
        {
            return new Vector3(a.x, a.y, a.x);
        }

        public static Vector3 yyx(this Vector3 a)
        {
            return new Vector3(a.y, a.y, a.x);
        }

        public static Vector3 zyx(this Vector3 a)
        {
            return new Vector3(a.z, a.y, a.x);
        }

        public static Vector3 _1zx(this Vector3 a)
        {
            return new Vector3(1.0f, a.z, a.x);
        }

        public static Vector3 _0zx(this Vector3 a)
        {
            return new Vector3(0.0f, a.z, a.x);
        }

        public static Vector3 xzx(this Vector3 a)
        {
            return new Vector3(a.x, a.z, a.x);
        }

        public static Vector3 yzx(this Vector3 a)
        {
            return new Vector3(a.y, a.z, a.x);
        }

        public static Vector3 zzx(this Vector3 a)
        {
            return new Vector3(a.z, a.z, a.x);
        }

        public static Vector3 _11y(this Vector3 a)
        {
            return new Vector3(1.0f, 1.0f, a.y);
        }

        public static Vector3 _01y(this Vector3 a)
        {
            return new Vector3(0.0f, 1.0f, a.y);
        }

        public static Vector3 x1y(this Vector3 a)
        {
            return new Vector3(a.x, 1.0f, a.y);
        }

        public static Vector3 y1y(this Vector3 a)
        {
            return new Vector3(a.y, 1.0f, a.y);
        }

        public static Vector3 z1y(this Vector3 a)
        {
            return new Vector3(a.z, 1.0f, a.y);
        }

        public static Vector3 _10y(this Vector3 a)
        {
            return new Vector3(1.0f, 0.0f, a.y);
        }

        public static Vector3 _00y(this Vector3 a)
        {
            return new Vector3(0.0f, 0.0f, a.y);
        }

        public static Vector3 x0y(this Vector3 a)
        {
            return new Vector3(a.x, 0.0f, a.y);
        }

        public static Vector3 y0y(this Vector3 a)
        {
            return new Vector3(a.y, 0.0f, a.y);
        }

        public static Vector3 z0y(this Vector3 a)
        {
            return new Vector3(a.z, 0.0f, a.y);
        }

        public static Vector3 _1xy(this Vector3 a)
        {
            return new Vector3(1.0f, a.x, a.y);
        }

        public static Vector3 _0xy(this Vector3 a)
        {
            return new Vector3(0.0f, a.x, a.y);
        }

        public static Vector3 xxy(this Vector3 a)
        {
            return new Vector3(a.x, a.x, a.y);
        }

        public static Vector3 yxy(this Vector3 a)
        {
            return new Vector3(a.y, a.x, a.y);
        }

        public static Vector3 zxy(this Vector3 a)
        {
            return new Vector3(a.z, a.x, a.y);
        }

        public static Vector3 _1yy(this Vector3 a)
        {
            return new Vector3(1.0f, a.y, a.y);
        }

        public static Vector3 _0yy(this Vector3 a)
        {
            return new Vector3(0.0f, a.y, a.y);
        }

        public static Vector3 xyy(this Vector3 a)
        {
            return new Vector3(a.x, a.y, a.y);
        }

        public static Vector3 yyy(this Vector3 a)
        {
            return new Vector3(a.y, a.y, a.y);
        }

        public static Vector3 zyy(this Vector3 a)
        {
            return new Vector3(a.z, a.y, a.y);
        }

        public static Vector3 _1zy(this Vector3 a)
        {
            return new Vector3(1.0f, a.z, a.y);
        }

        public static Vector3 _0zy(this Vector3 a)
        {
            return new Vector3(0.0f, a.z, a.y);
        }

        public static Vector3 xzy(this Vector3 a)
        {
            return new Vector3(a.x, a.z, a.y);
        }

        public static Vector3 yzy(this Vector3 a)
        {
            return new Vector3(a.y, a.z, a.y);
        }

        public static Vector3 zzy(this Vector3 a)
        {
            return new Vector3(a.z, a.z, a.y);
        }

        public static Vector3 _11z(this Vector3 a)
        {
            return new Vector3(1.0f, 1.0f, a.z);
        }

        public static Vector3 _01z(this Vector3 a)
        {
            return new Vector3(0.0f, 1.0f, a.z);
        }

        public static Vector3 x1z(this Vector3 a)
        {
            return new Vector3(a.x, 1.0f, a.z);
        }

        public static Vector3 y1z(this Vector3 a)
        {
            return new Vector3(a.y, 1.0f, a.z);
        }

        public static Vector3 z1z(this Vector3 a)
        {
            return new Vector3(a.z, 1.0f, a.z);
        }

        public static Vector3 _10z(this Vector3 a)
        {
            return new Vector3(1.0f, 0.0f, a.z);
        }

        public static Vector3 _00z(this Vector3 a)
        {
            return new Vector3(0.0f, 0.0f, a.z);
        }

        public static Vector3 x0z(this Vector3 a)
        {
            return new Vector3(a.x, 0.0f, a.z);
        }

        public static Vector3 y0z(this Vector3 a)
        {
            return new Vector3(a.y, 0.0f, a.z);
        }

        public static Vector3 z0z(this Vector3 a)
        {
            return new Vector3(a.z, 0.0f, a.z);
        }

        public static Vector3 _1xz(this Vector3 a)
        {
            return new Vector3(1.0f, a.x, a.z);
        }

        public static Vector3 _0xz(this Vector3 a)
        {
            return new Vector3(0.0f, a.x, a.z);
        }

        public static Vector3 xxz(this Vector3 a)
        {
            return new Vector3(a.x, a.x, a.z);
        }

        public static Vector3 yxz(this Vector3 a)
        {
            return new Vector3(a.y, a.x, a.z);
        }

        public static Vector3 zxz(this Vector3 a)
        {
            return new Vector3(a.z, a.x, a.z);
        }

        public static Vector3 _1yz(this Vector3 a)
        {
            return new Vector3(1.0f, a.y, a.z);
        }

        public static Vector3 _0yz(this Vector3 a)
        {
            return new Vector3(0.0f, a.y, a.z);
        }

        public static Vector3 xyz(this Vector3 a)
        {
            return new Vector3(a.x, a.y, a.z);
        }

        public static Vector3 yyz(this Vector3 a)
        {
            return new Vector3(a.y, a.y, a.z);
        }

        public static Vector3 zyz(this Vector3 a)
        {
            return new Vector3(a.z, a.y, a.z);
        }

        public static Vector3 _1zz(this Vector3 a)
        {
            return new Vector3(1.0f, a.z, a.z);
        }

        public static Vector3 _0zz(this Vector3 a)
        {
            return new Vector3(0.0f, a.z, a.z);
        }

        public static Vector3 xzz(this Vector3 a)
        {
            return new Vector3(a.x, a.z, a.z);
        }

        public static Vector3 yzz(this Vector3 a)
        {
            return new Vector3(a.y, a.z, a.z);
        }

        public static Vector3 zzz(this Vector3 a)
        {
            return new Vector3(a.z, a.z, a.z);
        }

        //swizzles of size 4
        public static Vector4 _1111(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 _0111(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 x111(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 y111(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 z111(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 _1011(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 _0011(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 x011(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 y011(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 z011(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 _1x11(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, 1.0f);
        }

        public static Vector4 _0x11(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, 1.0f);
        }

        public static Vector4 xx11(this Vector3 a)
        {
            return new Vector4(a.x, a.x, 1.0f, 1.0f);
        }

        public static Vector4 yx11(this Vector3 a)
        {
            return new Vector4(a.y, a.x, 1.0f, 1.0f);
        }

        public static Vector4 zx11(this Vector3 a)
        {
            return new Vector4(a.z, a.x, 1.0f, 1.0f);
        }

        public static Vector4 _1y11(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, 1.0f);
        }

        public static Vector4 _0y11(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, 1.0f);
        }

        public static Vector4 xy11(this Vector3 a)
        {
            return new Vector4(a.x, a.y, 1.0f, 1.0f);
        }

        public static Vector4 yy11(this Vector3 a)
        {
            return new Vector4(a.y, a.y, 1.0f, 1.0f);
        }

        public static Vector4 zy11(this Vector3 a)
        {
            return new Vector4(a.z, a.y, 1.0f, 1.0f);
        }

        public static Vector4 _1z11(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, 1.0f);
        }

        public static Vector4 _0z11(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, 1.0f);
        }

        public static Vector4 xz11(this Vector3 a)
        {
            return new Vector4(a.x, a.z, 1.0f, 1.0f);
        }

        public static Vector4 yz11(this Vector3 a)
        {
            return new Vector4(a.y, a.z, 1.0f, 1.0f);
        }

        public static Vector4 zz11(this Vector3 a)
        {
            return new Vector4(a.z, a.z, 1.0f, 1.0f);
        }

        public static Vector4 _1101(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 _0101(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 x101(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 y101(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 z101(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 _1001(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 _0001(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 x001(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 y001(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 z001(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 _1x01(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, 1.0f);
        }

        public static Vector4 _0x01(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, 1.0f);
        }

        public static Vector4 xx01(this Vector3 a)
        {
            return new Vector4(a.x, a.x, 0.0f, 1.0f);
        }

        public static Vector4 yx01(this Vector3 a)
        {
            return new Vector4(a.y, a.x, 0.0f, 1.0f);
        }

        public static Vector4 zx01(this Vector3 a)
        {
            return new Vector4(a.z, a.x, 0.0f, 1.0f);
        }

        public static Vector4 _1y01(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, 1.0f);
        }

        public static Vector4 _0y01(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, 1.0f);
        }

        public static Vector4 xy01(this Vector3 a)
        {
            return new Vector4(a.x, a.y, 0.0f, 1.0f);
        }

        public static Vector4 yy01(this Vector3 a)
        {
            return new Vector4(a.y, a.y, 0.0f, 1.0f);
        }

        public static Vector4 zy01(this Vector3 a)
        {
            return new Vector4(a.z, a.y, 0.0f, 1.0f);
        }

        public static Vector4 _1z01(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, 1.0f);
        }

        public static Vector4 _0z01(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, 1.0f);
        }

        public static Vector4 xz01(this Vector3 a)
        {
            return new Vector4(a.x, a.z, 0.0f, 1.0f);
        }

        public static Vector4 yz01(this Vector3 a)
        {
            return new Vector4(a.y, a.z, 0.0f, 1.0f);
        }

        public static Vector4 zz01(this Vector3 a)
        {
            return new Vector4(a.z, a.z, 0.0f, 1.0f);
        }

        public static Vector4 _11x1(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, 1.0f);
        }

        public static Vector4 _01x1(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, 1.0f);
        }

        public static Vector4 x1x1(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.x, 1.0f);
        }

        public static Vector4 y1x1(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.x, 1.0f);
        }

        public static Vector4 z1x1(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.x, 1.0f);
        }

        public static Vector4 _10x1(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, 1.0f);
        }

        public static Vector4 _00x1(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, 1.0f);
        }

        public static Vector4 x0x1(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.x, 1.0f);
        }

        public static Vector4 y0x1(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.x, 1.0f);
        }

        public static Vector4 z0x1(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.x, 1.0f);
        }

        public static Vector4 _1xx1(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.x, 1.0f);
        }

        public static Vector4 _0xx1(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.x, 1.0f);
        }

        public static Vector4 xxx1(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.x, 1.0f);
        }

        public static Vector4 yxx1(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.x, 1.0f);
        }

        public static Vector4 zxx1(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.x, 1.0f);
        }

        public static Vector4 _1yx1(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.x, 1.0f);
        }

        public static Vector4 _0yx1(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.x, 1.0f);
        }

        public static Vector4 xyx1(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.x, 1.0f);
        }

        public static Vector4 yyx1(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.x, 1.0f);
        }

        public static Vector4 zyx1(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.x, 1.0f);
        }

        public static Vector4 _1zx1(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.x, 1.0f);
        }

        public static Vector4 _0zx1(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.x, 1.0f);
        }

        public static Vector4 xzx1(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.x, 1.0f);
        }

        public static Vector4 yzx1(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.x, 1.0f);
        }

        public static Vector4 zzx1(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.x, 1.0f);
        }

        public static Vector4 _11y1(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, 1.0f);
        }

        public static Vector4 _01y1(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, 1.0f);
        }

        public static Vector4 x1y1(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.y, 1.0f);
        }

        public static Vector4 y1y1(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.y, 1.0f);
        }

        public static Vector4 z1y1(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.y, 1.0f);
        }

        public static Vector4 _10y1(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, 1.0f);
        }

        public static Vector4 _00y1(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, 1.0f);
        }

        public static Vector4 x0y1(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.y, 1.0f);
        }

        public static Vector4 y0y1(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.y, 1.0f);
        }

        public static Vector4 z0y1(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.y, 1.0f);
        }

        public static Vector4 _1xy1(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.y, 1.0f);
        }

        public static Vector4 _0xy1(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.y, 1.0f);
        }

        public static Vector4 xxy1(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.y, 1.0f);
        }

        public static Vector4 yxy1(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.y, 1.0f);
        }

        public static Vector4 zxy1(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.y, 1.0f);
        }

        public static Vector4 _1yy1(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.y, 1.0f);
        }

        public static Vector4 _0yy1(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.y, 1.0f);
        }

        public static Vector4 xyy1(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.y, 1.0f);
        }

        public static Vector4 yyy1(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.y, 1.0f);
        }

        public static Vector4 zyy1(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.y, 1.0f);
        }

        public static Vector4 _1zy1(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.y, 1.0f);
        }

        public static Vector4 _0zy1(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.y, 1.0f);
        }

        public static Vector4 xzy1(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.y, 1.0f);
        }

        public static Vector4 yzy1(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.y, 1.0f);
        }

        public static Vector4 zzy1(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.y, 1.0f);
        }

        public static Vector4 _11z1(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, 1.0f);
        }

        public static Vector4 _01z1(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, 1.0f);
        }

        public static Vector4 x1z1(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.z, 1.0f);
        }

        public static Vector4 y1z1(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.z, 1.0f);
        }

        public static Vector4 z1z1(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.z, 1.0f);
        }

        public static Vector4 _10z1(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, 1.0f);
        }

        public static Vector4 _00z1(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, 1.0f);
        }

        public static Vector4 x0z1(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.z, 1.0f);
        }

        public static Vector4 y0z1(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.z, 1.0f);
        }

        public static Vector4 z0z1(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.z, 1.0f);
        }

        public static Vector4 _1xz1(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.z, 1.0f);
        }

        public static Vector4 _0xz1(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.z, 1.0f);
        }

        public static Vector4 xxz1(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.z, 1.0f);
        }

        public static Vector4 yxz1(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.z, 1.0f);
        }

        public static Vector4 zxz1(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.z, 1.0f);
        }

        public static Vector4 _1yz1(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.z, 1.0f);
        }

        public static Vector4 _0yz1(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.z, 1.0f);
        }

        public static Vector4 xyz1(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.z, 1.0f);
        }

        public static Vector4 yyz1(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.z, 1.0f);
        }

        public static Vector4 zyz1(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.z, 1.0f);
        }

        public static Vector4 _1zz1(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.z, 1.0f);
        }

        public static Vector4 _0zz1(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.z, 1.0f);
        }

        public static Vector4 xzz1(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.z, 1.0f);
        }

        public static Vector4 yzz1(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.z, 1.0f);
        }

        public static Vector4 zzz1(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.z, 1.0f);
        }

        public static Vector4 _1110(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 _0110(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 x110(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 y110(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 z110(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 _1010(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 _0010(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 x010(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 y010(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 z010(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 _1x10(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, 0.0f);
        }

        public static Vector4 _0x10(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, 0.0f);
        }

        public static Vector4 xx10(this Vector3 a)
        {
            return new Vector4(a.x, a.x, 1.0f, 0.0f);
        }

        public static Vector4 yx10(this Vector3 a)
        {
            return new Vector4(a.y, a.x, 1.0f, 0.0f);
        }

        public static Vector4 zx10(this Vector3 a)
        {
            return new Vector4(a.z, a.x, 1.0f, 0.0f);
        }

        public static Vector4 _1y10(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, 0.0f);
        }

        public static Vector4 _0y10(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, 0.0f);
        }

        public static Vector4 xy10(this Vector3 a)
        {
            return new Vector4(a.x, a.y, 1.0f, 0.0f);
        }

        public static Vector4 yy10(this Vector3 a)
        {
            return new Vector4(a.y, a.y, 1.0f, 0.0f);
        }

        public static Vector4 zy10(this Vector3 a)
        {
            return new Vector4(a.z, a.y, 1.0f, 0.0f);
        }

        public static Vector4 _1z10(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, 0.0f);
        }

        public static Vector4 _0z10(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, 0.0f);
        }

        public static Vector4 xz10(this Vector3 a)
        {
            return new Vector4(a.x, a.z, 1.0f, 0.0f);
        }

        public static Vector4 yz10(this Vector3 a)
        {
            return new Vector4(a.y, a.z, 1.0f, 0.0f);
        }

        public static Vector4 zz10(this Vector3 a)
        {
            return new Vector4(a.z, a.z, 1.0f, 0.0f);
        }

        public static Vector4 _1100(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 _0100(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 x100(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 y100(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 z100(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 _1000(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 _0000(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 x000(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 y000(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 z000(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 _1x00(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, 0.0f);
        }

        public static Vector4 _0x00(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, 0.0f);
        }

        public static Vector4 xx00(this Vector3 a)
        {
            return new Vector4(a.x, a.x, 0.0f, 0.0f);
        }

        public static Vector4 yx00(this Vector3 a)
        {
            return new Vector4(a.y, a.x, 0.0f, 0.0f);
        }

        public static Vector4 zx00(this Vector3 a)
        {
            return new Vector4(a.z, a.x, 0.0f, 0.0f);
        }

        public static Vector4 _1y00(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, 0.0f);
        }

        public static Vector4 _0y00(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, 0.0f);
        }

        public static Vector4 xy00(this Vector3 a)
        {
            return new Vector4(a.x, a.y, 0.0f, 0.0f);
        }

        public static Vector4 yy00(this Vector3 a)
        {
            return new Vector4(a.y, a.y, 0.0f, 0.0f);
        }

        public static Vector4 zy00(this Vector3 a)
        {
            return new Vector4(a.z, a.y, 0.0f, 0.0f);
        }

        public static Vector4 _1z00(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, 0.0f);
        }

        public static Vector4 _0z00(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, 0.0f);
        }

        public static Vector4 xz00(this Vector3 a)
        {
            return new Vector4(a.x, a.z, 0.0f, 0.0f);
        }

        public static Vector4 yz00(this Vector3 a)
        {
            return new Vector4(a.y, a.z, 0.0f, 0.0f);
        }

        public static Vector4 zz00(this Vector3 a)
        {
            return new Vector4(a.z, a.z, 0.0f, 0.0f);
        }

        public static Vector4 _11x0(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, 0.0f);
        }

        public static Vector4 _01x0(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, 0.0f);
        }

        public static Vector4 x1x0(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.x, 0.0f);
        }

        public static Vector4 y1x0(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.x, 0.0f);
        }

        public static Vector4 z1x0(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.x, 0.0f);
        }

        public static Vector4 _10x0(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, 0.0f);
        }

        public static Vector4 _00x0(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, 0.0f);
        }

        public static Vector4 x0x0(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.x, 0.0f);
        }

        public static Vector4 y0x0(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.x, 0.0f);
        }

        public static Vector4 z0x0(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.x, 0.0f);
        }

        public static Vector4 _1xx0(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.x, 0.0f);
        }

        public static Vector4 _0xx0(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.x, 0.0f);
        }

        public static Vector4 xxx0(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.x, 0.0f);
        }

        public static Vector4 yxx0(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.x, 0.0f);
        }

        public static Vector4 zxx0(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.x, 0.0f);
        }

        public static Vector4 _1yx0(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.x, 0.0f);
        }

        public static Vector4 _0yx0(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.x, 0.0f);
        }

        public static Vector4 xyx0(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.x, 0.0f);
        }

        public static Vector4 yyx0(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.x, 0.0f);
        }

        public static Vector4 zyx0(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.x, 0.0f);
        }

        public static Vector4 _1zx0(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.x, 0.0f);
        }

        public static Vector4 _0zx0(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.x, 0.0f);
        }

        public static Vector4 xzx0(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.x, 0.0f);
        }

        public static Vector4 yzx0(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.x, 0.0f);
        }

        public static Vector4 zzx0(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.x, 0.0f);
        }

        public static Vector4 _11y0(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, 0.0f);
        }

        public static Vector4 _01y0(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, 0.0f);
        }

        public static Vector4 x1y0(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.y, 0.0f);
        }

        public static Vector4 y1y0(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.y, 0.0f);
        }

        public static Vector4 z1y0(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.y, 0.0f);
        }

        public static Vector4 _10y0(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, 0.0f);
        }

        public static Vector4 _00y0(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, 0.0f);
        }

        public static Vector4 x0y0(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.y, 0.0f);
        }

        public static Vector4 y0y0(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.y, 0.0f);
        }

        public static Vector4 z0y0(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.y, 0.0f);
        }

        public static Vector4 _1xy0(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.y, 0.0f);
        }

        public static Vector4 _0xy0(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.y, 0.0f);
        }

        public static Vector4 xxy0(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.y, 0.0f);
        }

        public static Vector4 yxy0(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.y, 0.0f);
        }

        public static Vector4 zxy0(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.y, 0.0f);
        }

        public static Vector4 _1yy0(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.y, 0.0f);
        }

        public static Vector4 _0yy0(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.y, 0.0f);
        }

        public static Vector4 xyy0(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.y, 0.0f);
        }

        public static Vector4 yyy0(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.y, 0.0f);
        }

        public static Vector4 zyy0(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.y, 0.0f);
        }

        public static Vector4 _1zy0(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.y, 0.0f);
        }

        public static Vector4 _0zy0(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.y, 0.0f);
        }

        public static Vector4 xzy0(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.y, 0.0f);
        }

        public static Vector4 yzy0(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.y, 0.0f);
        }

        public static Vector4 zzy0(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.y, 0.0f);
        }

        public static Vector4 _11z0(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, 0.0f);
        }

        public static Vector4 _01z0(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, 0.0f);
        }

        public static Vector4 x1z0(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.z, 0.0f);
        }

        public static Vector4 y1z0(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.z, 0.0f);
        }

        public static Vector4 z1z0(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.z, 0.0f);
        }

        public static Vector4 _10z0(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, 0.0f);
        }

        public static Vector4 _00z0(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, 0.0f);
        }

        public static Vector4 x0z0(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.z, 0.0f);
        }

        public static Vector4 y0z0(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.z, 0.0f);
        }

        public static Vector4 z0z0(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.z, 0.0f);
        }

        public static Vector4 _1xz0(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.z, 0.0f);
        }

        public static Vector4 _0xz0(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.z, 0.0f);
        }

        public static Vector4 xxz0(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.z, 0.0f);
        }

        public static Vector4 yxz0(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.z, 0.0f);
        }

        public static Vector4 zxz0(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.z, 0.0f);
        }

        public static Vector4 _1yz0(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.z, 0.0f);
        }

        public static Vector4 _0yz0(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.z, 0.0f);
        }

        public static Vector4 xyz0(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.z, 0.0f);
        }

        public static Vector4 yyz0(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.z, 0.0f);
        }

        public static Vector4 zyz0(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.z, 0.0f);
        }

        public static Vector4 _1zz0(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.z, 0.0f);
        }

        public static Vector4 _0zz0(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.z, 0.0f);
        }

        public static Vector4 xzz0(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.z, 0.0f);
        }

        public static Vector4 yzz0(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.z, 0.0f);
        }

        public static Vector4 zzz0(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.z, 0.0f);
        }

        public static Vector4 _111x(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, a.x);
        }

        public static Vector4 _011x(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, a.x);
        }

        public static Vector4 x11x(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, a.x);
        }

        public static Vector4 y11x(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, a.x);
        }

        public static Vector4 z11x(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, a.x);
        }

        public static Vector4 _101x(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, a.x);
        }

        public static Vector4 _001x(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, a.x);
        }

        public static Vector4 x01x(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, a.x);
        }

        public static Vector4 y01x(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, a.x);
        }

        public static Vector4 z01x(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, a.x);
        }

        public static Vector4 _1x1x(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, a.x);
        }

        public static Vector4 _0x1x(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, a.x);
        }

        public static Vector4 xx1x(this Vector3 a)
        {
            return new Vector4(a.x, a.x, 1.0f, a.x);
        }

        public static Vector4 yx1x(this Vector3 a)
        {
            return new Vector4(a.y, a.x, 1.0f, a.x);
        }

        public static Vector4 zx1x(this Vector3 a)
        {
            return new Vector4(a.z, a.x, 1.0f, a.x);
        }

        public static Vector4 _1y1x(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, a.x);
        }

        public static Vector4 _0y1x(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, a.x);
        }

        public static Vector4 xy1x(this Vector3 a)
        {
            return new Vector4(a.x, a.y, 1.0f, a.x);
        }

        public static Vector4 yy1x(this Vector3 a)
        {
            return new Vector4(a.y, a.y, 1.0f, a.x);
        }

        public static Vector4 zy1x(this Vector3 a)
        {
            return new Vector4(a.z, a.y, 1.0f, a.x);
        }

        public static Vector4 _1z1x(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, a.x);
        }

        public static Vector4 _0z1x(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, a.x);
        }

        public static Vector4 xz1x(this Vector3 a)
        {
            return new Vector4(a.x, a.z, 1.0f, a.x);
        }

        public static Vector4 yz1x(this Vector3 a)
        {
            return new Vector4(a.y, a.z, 1.0f, a.x);
        }

        public static Vector4 zz1x(this Vector3 a)
        {
            return new Vector4(a.z, a.z, 1.0f, a.x);
        }

        public static Vector4 _110x(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, a.x);
        }

        public static Vector4 _010x(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, a.x);
        }

        public static Vector4 x10x(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, a.x);
        }

        public static Vector4 y10x(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, a.x);
        }

        public static Vector4 z10x(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, a.x);
        }

        public static Vector4 _100x(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, a.x);
        }

        public static Vector4 _000x(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, a.x);
        }

        public static Vector4 x00x(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, a.x);
        }

        public static Vector4 y00x(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, a.x);
        }

        public static Vector4 z00x(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, a.x);
        }

        public static Vector4 _1x0x(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, a.x);
        }

        public static Vector4 _0x0x(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, a.x);
        }

        public static Vector4 xx0x(this Vector3 a)
        {
            return new Vector4(a.x, a.x, 0.0f, a.x);
        }

        public static Vector4 yx0x(this Vector3 a)
        {
            return new Vector4(a.y, a.x, 0.0f, a.x);
        }

        public static Vector4 zx0x(this Vector3 a)
        {
            return new Vector4(a.z, a.x, 0.0f, a.x);
        }

        public static Vector4 _1y0x(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, a.x);
        }

        public static Vector4 _0y0x(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, a.x);
        }

        public static Vector4 xy0x(this Vector3 a)
        {
            return new Vector4(a.x, a.y, 0.0f, a.x);
        }

        public static Vector4 yy0x(this Vector3 a)
        {
            return new Vector4(a.y, a.y, 0.0f, a.x);
        }

        public static Vector4 zy0x(this Vector3 a)
        {
            return new Vector4(a.z, a.y, 0.0f, a.x);
        }

        public static Vector4 _1z0x(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, a.x);
        }

        public static Vector4 _0z0x(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, a.x);
        }

        public static Vector4 xz0x(this Vector3 a)
        {
            return new Vector4(a.x, a.z, 0.0f, a.x);
        }

        public static Vector4 yz0x(this Vector3 a)
        {
            return new Vector4(a.y, a.z, 0.0f, a.x);
        }

        public static Vector4 zz0x(this Vector3 a)
        {
            return new Vector4(a.z, a.z, 0.0f, a.x);
        }

        public static Vector4 _11xx(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, a.x);
        }

        public static Vector4 _01xx(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, a.x);
        }

        public static Vector4 x1xx(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.x, a.x);
        }

        public static Vector4 y1xx(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.x, a.x);
        }

        public static Vector4 z1xx(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.x, a.x);
        }

        public static Vector4 _10xx(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, a.x);
        }

        public static Vector4 _00xx(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, a.x);
        }

        public static Vector4 x0xx(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.x, a.x);
        }

        public static Vector4 y0xx(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.x, a.x);
        }

        public static Vector4 z0xx(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.x, a.x);
        }

        public static Vector4 _1xxx(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.x, a.x);
        }

        public static Vector4 _0xxx(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.x, a.x);
        }

        public static Vector4 xxxx(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.x, a.x);
        }

        public static Vector4 yxxx(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.x, a.x);
        }

        public static Vector4 zxxx(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.x, a.x);
        }

        public static Vector4 _1yxx(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.x, a.x);
        }

        public static Vector4 _0yxx(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.x, a.x);
        }

        public static Vector4 xyxx(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.x, a.x);
        }

        public static Vector4 yyxx(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.x, a.x);
        }

        public static Vector4 zyxx(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.x, a.x);
        }

        public static Vector4 _1zxx(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.x, a.x);
        }

        public static Vector4 _0zxx(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.x, a.x);
        }

        public static Vector4 xzxx(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.x, a.x);
        }

        public static Vector4 yzxx(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.x, a.x);
        }

        public static Vector4 zzxx(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.x, a.x);
        }

        public static Vector4 _11yx(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, a.x);
        }

        public static Vector4 _01yx(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, a.x);
        }

        public static Vector4 x1yx(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.y, a.x);
        }

        public static Vector4 y1yx(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.y, a.x);
        }

        public static Vector4 z1yx(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.y, a.x);
        }

        public static Vector4 _10yx(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, a.x);
        }

        public static Vector4 _00yx(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, a.x);
        }

        public static Vector4 x0yx(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.y, a.x);
        }

        public static Vector4 y0yx(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.y, a.x);
        }

        public static Vector4 z0yx(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.y, a.x);
        }

        public static Vector4 _1xyx(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.y, a.x);
        }

        public static Vector4 _0xyx(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.y, a.x);
        }

        public static Vector4 xxyx(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.y, a.x);
        }

        public static Vector4 yxyx(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.y, a.x);
        }

        public static Vector4 zxyx(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.y, a.x);
        }

        public static Vector4 _1yyx(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.y, a.x);
        }

        public static Vector4 _0yyx(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.y, a.x);
        }

        public static Vector4 xyyx(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.y, a.x);
        }

        public static Vector4 yyyx(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.y, a.x);
        }

        public static Vector4 zyyx(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.y, a.x);
        }

        public static Vector4 _1zyx(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.y, a.x);
        }

        public static Vector4 _0zyx(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.y, a.x);
        }

        public static Vector4 xzyx(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.y, a.x);
        }

        public static Vector4 yzyx(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.y, a.x);
        }

        public static Vector4 zzyx(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.y, a.x);
        }

        public static Vector4 _11zx(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, a.x);
        }

        public static Vector4 _01zx(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, a.x);
        }

        public static Vector4 x1zx(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.z, a.x);
        }

        public static Vector4 y1zx(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.z, a.x);
        }

        public static Vector4 z1zx(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.z, a.x);
        }

        public static Vector4 _10zx(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, a.x);
        }

        public static Vector4 _00zx(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, a.x);
        }

        public static Vector4 x0zx(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.z, a.x);
        }

        public static Vector4 y0zx(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.z, a.x);
        }

        public static Vector4 z0zx(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.z, a.x);
        }

        public static Vector4 _1xzx(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.z, a.x);
        }

        public static Vector4 _0xzx(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.z, a.x);
        }

        public static Vector4 xxzx(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.z, a.x);
        }

        public static Vector4 yxzx(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.z, a.x);
        }

        public static Vector4 zxzx(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.z, a.x);
        }

        public static Vector4 _1yzx(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.z, a.x);
        }

        public static Vector4 _0yzx(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.z, a.x);
        }

        public static Vector4 xyzx(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.z, a.x);
        }

        public static Vector4 yyzx(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.z, a.x);
        }

        public static Vector4 zyzx(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.z, a.x);
        }

        public static Vector4 _1zzx(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.z, a.x);
        }

        public static Vector4 _0zzx(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.z, a.x);
        }

        public static Vector4 xzzx(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.z, a.x);
        }

        public static Vector4 yzzx(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.z, a.x);
        }

        public static Vector4 zzzx(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.z, a.x);
        }

        public static Vector4 _111y(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, a.y);
        }

        public static Vector4 _011y(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, a.y);
        }

        public static Vector4 x11y(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, a.y);
        }

        public static Vector4 y11y(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, a.y);
        }

        public static Vector4 z11y(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, a.y);
        }

        public static Vector4 _101y(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, a.y);
        }

        public static Vector4 _001y(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, a.y);
        }

        public static Vector4 x01y(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, a.y);
        }

        public static Vector4 y01y(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, a.y);
        }

        public static Vector4 z01y(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, a.y);
        }

        public static Vector4 _1x1y(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, a.y);
        }

        public static Vector4 _0x1y(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, a.y);
        }

        public static Vector4 xx1y(this Vector3 a)
        {
            return new Vector4(a.x, a.x, 1.0f, a.y);
        }

        public static Vector4 yx1y(this Vector3 a)
        {
            return new Vector4(a.y, a.x, 1.0f, a.y);
        }

        public static Vector4 zx1y(this Vector3 a)
        {
            return new Vector4(a.z, a.x, 1.0f, a.y);
        }

        public static Vector4 _1y1y(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, a.y);
        }

        public static Vector4 _0y1y(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, a.y);
        }

        public static Vector4 xy1y(this Vector3 a)
        {
            return new Vector4(a.x, a.y, 1.0f, a.y);
        }

        public static Vector4 yy1y(this Vector3 a)
        {
            return new Vector4(a.y, a.y, 1.0f, a.y);
        }

        public static Vector4 zy1y(this Vector3 a)
        {
            return new Vector4(a.z, a.y, 1.0f, a.y);
        }

        public static Vector4 _1z1y(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, a.y);
        }

        public static Vector4 _0z1y(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, a.y);
        }

        public static Vector4 xz1y(this Vector3 a)
        {
            return new Vector4(a.x, a.z, 1.0f, a.y);
        }

        public static Vector4 yz1y(this Vector3 a)
        {
            return new Vector4(a.y, a.z, 1.0f, a.y);
        }

        public static Vector4 zz1y(this Vector3 a)
        {
            return new Vector4(a.z, a.z, 1.0f, a.y);
        }

        public static Vector4 _110y(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, a.y);
        }

        public static Vector4 _010y(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, a.y);
        }

        public static Vector4 x10y(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, a.y);
        }

        public static Vector4 y10y(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, a.y);
        }

        public static Vector4 z10y(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, a.y);
        }

        public static Vector4 _100y(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, a.y);
        }

        public static Vector4 _000y(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, a.y);
        }

        public static Vector4 x00y(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, a.y);
        }

        public static Vector4 y00y(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, a.y);
        }

        public static Vector4 z00y(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, a.y);
        }

        public static Vector4 _1x0y(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, a.y);
        }

        public static Vector4 _0x0y(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, a.y);
        }

        public static Vector4 xx0y(this Vector3 a)
        {
            return new Vector4(a.x, a.x, 0.0f, a.y);
        }

        public static Vector4 yx0y(this Vector3 a)
        {
            return new Vector4(a.y, a.x, 0.0f, a.y);
        }

        public static Vector4 zx0y(this Vector3 a)
        {
            return new Vector4(a.z, a.x, 0.0f, a.y);
        }

        public static Vector4 _1y0y(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, a.y);
        }

        public static Vector4 _0y0y(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, a.y);
        }

        public static Vector4 xy0y(this Vector3 a)
        {
            return new Vector4(a.x, a.y, 0.0f, a.y);
        }

        public static Vector4 yy0y(this Vector3 a)
        {
            return new Vector4(a.y, a.y, 0.0f, a.y);
        }

        public static Vector4 zy0y(this Vector3 a)
        {
            return new Vector4(a.z, a.y, 0.0f, a.y);
        }

        public static Vector4 _1z0y(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, a.y);
        }

        public static Vector4 _0z0y(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, a.y);
        }

        public static Vector4 xz0y(this Vector3 a)
        {
            return new Vector4(a.x, a.z, 0.0f, a.y);
        }

        public static Vector4 yz0y(this Vector3 a)
        {
            return new Vector4(a.y, a.z, 0.0f, a.y);
        }

        public static Vector4 zz0y(this Vector3 a)
        {
            return new Vector4(a.z, a.z, 0.0f, a.y);
        }

        public static Vector4 _11xy(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, a.y);
        }

        public static Vector4 _01xy(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, a.y);
        }

        public static Vector4 x1xy(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.x, a.y);
        }

        public static Vector4 y1xy(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.x, a.y);
        }

        public static Vector4 z1xy(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.x, a.y);
        }

        public static Vector4 _10xy(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, a.y);
        }

        public static Vector4 _00xy(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, a.y);
        }

        public static Vector4 x0xy(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.x, a.y);
        }

        public static Vector4 y0xy(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.x, a.y);
        }

        public static Vector4 z0xy(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.x, a.y);
        }

        public static Vector4 _1xxy(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.x, a.y);
        }

        public static Vector4 _0xxy(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.x, a.y);
        }

        public static Vector4 xxxy(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.x, a.y);
        }

        public static Vector4 yxxy(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.x, a.y);
        }

        public static Vector4 zxxy(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.x, a.y);
        }

        public static Vector4 _1yxy(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.x, a.y);
        }

        public static Vector4 _0yxy(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.x, a.y);
        }

        public static Vector4 xyxy(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.x, a.y);
        }

        public static Vector4 yyxy(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.x, a.y);
        }

        public static Vector4 zyxy(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.x, a.y);
        }

        public static Vector4 _1zxy(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.x, a.y);
        }

        public static Vector4 _0zxy(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.x, a.y);
        }

        public static Vector4 xzxy(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.x, a.y);
        }

        public static Vector4 yzxy(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.x, a.y);
        }

        public static Vector4 zzxy(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.x, a.y);
        }

        public static Vector4 _11yy(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, a.y);
        }

        public static Vector4 _01yy(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, a.y);
        }

        public static Vector4 x1yy(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.y, a.y);
        }

        public static Vector4 y1yy(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.y, a.y);
        }

        public static Vector4 z1yy(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.y, a.y);
        }

        public static Vector4 _10yy(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, a.y);
        }

        public static Vector4 _00yy(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, a.y);
        }

        public static Vector4 x0yy(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.y, a.y);
        }

        public static Vector4 y0yy(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.y, a.y);
        }

        public static Vector4 z0yy(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.y, a.y);
        }

        public static Vector4 _1xyy(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.y, a.y);
        }

        public static Vector4 _0xyy(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.y, a.y);
        }

        public static Vector4 xxyy(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.y, a.y);
        }

        public static Vector4 yxyy(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.y, a.y);
        }

        public static Vector4 zxyy(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.y, a.y);
        }

        public static Vector4 _1yyy(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.y, a.y);
        }

        public static Vector4 _0yyy(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.y, a.y);
        }

        public static Vector4 xyyy(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.y, a.y);
        }

        public static Vector4 yyyy(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.y, a.y);
        }

        public static Vector4 zyyy(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.y, a.y);
        }

        public static Vector4 _1zyy(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.y, a.y);
        }

        public static Vector4 _0zyy(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.y, a.y);
        }

        public static Vector4 xzyy(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.y, a.y);
        }

        public static Vector4 yzyy(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.y, a.y);
        }

        public static Vector4 zzyy(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.y, a.y);
        }

        public static Vector4 _11zy(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, a.y);
        }

        public static Vector4 _01zy(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, a.y);
        }

        public static Vector4 x1zy(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.z, a.y);
        }

        public static Vector4 y1zy(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.z, a.y);
        }

        public static Vector4 z1zy(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.z, a.y);
        }

        public static Vector4 _10zy(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, a.y);
        }

        public static Vector4 _00zy(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, a.y);
        }

        public static Vector4 x0zy(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.z, a.y);
        }

        public static Vector4 y0zy(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.z, a.y);
        }

        public static Vector4 z0zy(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.z, a.y);
        }

        public static Vector4 _1xzy(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.z, a.y);
        }

        public static Vector4 _0xzy(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.z, a.y);
        }

        public static Vector4 xxzy(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.z, a.y);
        }

        public static Vector4 yxzy(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.z, a.y);
        }

        public static Vector4 zxzy(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.z, a.y);
        }

        public static Vector4 _1yzy(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.z, a.y);
        }

        public static Vector4 _0yzy(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.z, a.y);
        }

        public static Vector4 xyzy(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.z, a.y);
        }

        public static Vector4 yyzy(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.z, a.y);
        }

        public static Vector4 zyzy(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.z, a.y);
        }

        public static Vector4 _1zzy(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.z, a.y);
        }

        public static Vector4 _0zzy(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.z, a.y);
        }

        public static Vector4 xzzy(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.z, a.y);
        }

        public static Vector4 yzzy(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.z, a.y);
        }

        public static Vector4 zzzy(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.z, a.y);
        }

        public static Vector4 _111z(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, a.z);
        }

        public static Vector4 _011z(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, a.z);
        }

        public static Vector4 x11z(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, a.z);
        }

        public static Vector4 y11z(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, a.z);
        }

        public static Vector4 z11z(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, a.z);
        }

        public static Vector4 _101z(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, a.z);
        }

        public static Vector4 _001z(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, a.z);
        }

        public static Vector4 x01z(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, a.z);
        }

        public static Vector4 y01z(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, a.z);
        }

        public static Vector4 z01z(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, a.z);
        }

        public static Vector4 _1x1z(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, a.z);
        }

        public static Vector4 _0x1z(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, a.z);
        }

        public static Vector4 xx1z(this Vector3 a)
        {
            return new Vector4(a.x, a.x, 1.0f, a.z);
        }

        public static Vector4 yx1z(this Vector3 a)
        {
            return new Vector4(a.y, a.x, 1.0f, a.z);
        }

        public static Vector4 zx1z(this Vector3 a)
        {
            return new Vector4(a.z, a.x, 1.0f, a.z);
        }

        public static Vector4 _1y1z(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, a.z);
        }

        public static Vector4 _0y1z(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, a.z);
        }

        public static Vector4 xy1z(this Vector3 a)
        {
            return new Vector4(a.x, a.y, 1.0f, a.z);
        }

        public static Vector4 yy1z(this Vector3 a)
        {
            return new Vector4(a.y, a.y, 1.0f, a.z);
        }

        public static Vector4 zy1z(this Vector3 a)
        {
            return new Vector4(a.z, a.y, 1.0f, a.z);
        }

        public static Vector4 _1z1z(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, a.z);
        }

        public static Vector4 _0z1z(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, a.z);
        }

        public static Vector4 xz1z(this Vector3 a)
        {
            return new Vector4(a.x, a.z, 1.0f, a.z);
        }

        public static Vector4 yz1z(this Vector3 a)
        {
            return new Vector4(a.y, a.z, 1.0f, a.z);
        }

        public static Vector4 zz1z(this Vector3 a)
        {
            return new Vector4(a.z, a.z, 1.0f, a.z);
        }

        public static Vector4 _110z(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, a.z);
        }

        public static Vector4 _010z(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, a.z);
        }

        public static Vector4 x10z(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, a.z);
        }

        public static Vector4 y10z(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, a.z);
        }

        public static Vector4 z10z(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, a.z);
        }

        public static Vector4 _100z(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, a.z);
        }

        public static Vector4 _000z(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, a.z);
        }

        public static Vector4 x00z(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, a.z);
        }

        public static Vector4 y00z(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, a.z);
        }

        public static Vector4 z00z(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, a.z);
        }

        public static Vector4 _1x0z(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, a.z);
        }

        public static Vector4 _0x0z(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, a.z);
        }

        public static Vector4 xx0z(this Vector3 a)
        {
            return new Vector4(a.x, a.x, 0.0f, a.z);
        }

        public static Vector4 yx0z(this Vector3 a)
        {
            return new Vector4(a.y, a.x, 0.0f, a.z);
        }

        public static Vector4 zx0z(this Vector3 a)
        {
            return new Vector4(a.z, a.x, 0.0f, a.z);
        }

        public static Vector4 _1y0z(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, a.z);
        }

        public static Vector4 _0y0z(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, a.z);
        }

        public static Vector4 xy0z(this Vector3 a)
        {
            return new Vector4(a.x, a.y, 0.0f, a.z);
        }

        public static Vector4 yy0z(this Vector3 a)
        {
            return new Vector4(a.y, a.y, 0.0f, a.z);
        }

        public static Vector4 zy0z(this Vector3 a)
        {
            return new Vector4(a.z, a.y, 0.0f, a.z);
        }

        public static Vector4 _1z0z(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, a.z);
        }

        public static Vector4 _0z0z(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, a.z);
        }

        public static Vector4 xz0z(this Vector3 a)
        {
            return new Vector4(a.x, a.z, 0.0f, a.z);
        }

        public static Vector4 yz0z(this Vector3 a)
        {
            return new Vector4(a.y, a.z, 0.0f, a.z);
        }

        public static Vector4 zz0z(this Vector3 a)
        {
            return new Vector4(a.z, a.z, 0.0f, a.z);
        }

        public static Vector4 _11xz(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, a.z);
        }

        public static Vector4 _01xz(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, a.z);
        }

        public static Vector4 x1xz(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.x, a.z);
        }

        public static Vector4 y1xz(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.x, a.z);
        }

        public static Vector4 z1xz(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.x, a.z);
        }

        public static Vector4 _10xz(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, a.z);
        }

        public static Vector4 _00xz(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, a.z);
        }

        public static Vector4 x0xz(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.x, a.z);
        }

        public static Vector4 y0xz(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.x, a.z);
        }

        public static Vector4 z0xz(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.x, a.z);
        }

        public static Vector4 _1xxz(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.x, a.z);
        }

        public static Vector4 _0xxz(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.x, a.z);
        }

        public static Vector4 xxxz(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.x, a.z);
        }

        public static Vector4 yxxz(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.x, a.z);
        }

        public static Vector4 zxxz(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.x, a.z);
        }

        public static Vector4 _1yxz(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.x, a.z);
        }

        public static Vector4 _0yxz(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.x, a.z);
        }

        public static Vector4 xyxz(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.x, a.z);
        }

        public static Vector4 yyxz(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.x, a.z);
        }

        public static Vector4 zyxz(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.x, a.z);
        }

        public static Vector4 _1zxz(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.x, a.z);
        }

        public static Vector4 _0zxz(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.x, a.z);
        }

        public static Vector4 xzxz(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.x, a.z);
        }

        public static Vector4 yzxz(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.x, a.z);
        }

        public static Vector4 zzxz(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.x, a.z);
        }

        public static Vector4 _11yz(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, a.z);
        }

        public static Vector4 _01yz(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, a.z);
        }

        public static Vector4 x1yz(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.y, a.z);
        }

        public static Vector4 y1yz(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.y, a.z);
        }

        public static Vector4 z1yz(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.y, a.z);
        }

        public static Vector4 _10yz(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, a.z);
        }

        public static Vector4 _00yz(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, a.z);
        }

        public static Vector4 x0yz(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.y, a.z);
        }

        public static Vector4 y0yz(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.y, a.z);
        }

        public static Vector4 z0yz(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.y, a.z);
        }

        public static Vector4 _1xyz(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.y, a.z);
        }

        public static Vector4 _0xyz(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.y, a.z);
        }

        public static Vector4 xxyz(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.y, a.z);
        }

        public static Vector4 yxyz(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.y, a.z);
        }

        public static Vector4 zxyz(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.y, a.z);
        }

        public static Vector4 _1yyz(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.y, a.z);
        }

        public static Vector4 _0yyz(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.y, a.z);
        }

        public static Vector4 xyyz(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.y, a.z);
        }

        public static Vector4 yyyz(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.y, a.z);
        }

        public static Vector4 zyyz(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.y, a.z);
        }

        public static Vector4 _1zyz(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.y, a.z);
        }

        public static Vector4 _0zyz(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.y, a.z);
        }

        public static Vector4 xzyz(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.y, a.z);
        }

        public static Vector4 yzyz(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.y, a.z);
        }

        public static Vector4 zzyz(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.y, a.z);
        }

        public static Vector4 _11zz(this Vector3 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, a.z);
        }

        public static Vector4 _01zz(this Vector3 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, a.z);
        }

        public static Vector4 x1zz(this Vector3 a)
        {
            return new Vector4(a.x, 1.0f, a.z, a.z);
        }

        public static Vector4 y1zz(this Vector3 a)
        {
            return new Vector4(a.y, 1.0f, a.z, a.z);
        }

        public static Vector4 z1zz(this Vector3 a)
        {
            return new Vector4(a.z, 1.0f, a.z, a.z);
        }

        public static Vector4 _10zz(this Vector3 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, a.z);
        }

        public static Vector4 _00zz(this Vector3 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, a.z);
        }

        public static Vector4 x0zz(this Vector3 a)
        {
            return new Vector4(a.x, 0.0f, a.z, a.z);
        }

        public static Vector4 y0zz(this Vector3 a)
        {
            return new Vector4(a.y, 0.0f, a.z, a.z);
        }

        public static Vector4 z0zz(this Vector3 a)
        {
            return new Vector4(a.z, 0.0f, a.z, a.z);
        }

        public static Vector4 _1xzz(this Vector3 a)
        {
            return new Vector4(1.0f, a.x, a.z, a.z);
        }

        public static Vector4 _0xzz(this Vector3 a)
        {
            return new Vector4(0.0f, a.x, a.z, a.z);
        }

        public static Vector4 xxzz(this Vector3 a)
        {
            return new Vector4(a.x, a.x, a.z, a.z);
        }

        public static Vector4 yxzz(this Vector3 a)
        {
            return new Vector4(a.y, a.x, a.z, a.z);
        }

        public static Vector4 zxzz(this Vector3 a)
        {
            return new Vector4(a.z, a.x, a.z, a.z);
        }

        public static Vector4 _1yzz(this Vector3 a)
        {
            return new Vector4(1.0f, a.y, a.z, a.z);
        }

        public static Vector4 _0yzz(this Vector3 a)
        {
            return new Vector4(0.0f, a.y, a.z, a.z);
        }

        public static Vector4 xyzz(this Vector3 a)
        {
            return new Vector4(a.x, a.y, a.z, a.z);
        }

        public static Vector4 yyzz(this Vector3 a)
        {
            return new Vector4(a.y, a.y, a.z, a.z);
        }

        public static Vector4 zyzz(this Vector3 a)
        {
            return new Vector4(a.z, a.y, a.z, a.z);
        }

        public static Vector4 _1zzz(this Vector3 a)
        {
            return new Vector4(1.0f, a.z, a.z, a.z);
        }

        public static Vector4 _0zzz(this Vector3 a)
        {
            return new Vector4(0.0f, a.z, a.z, a.z);
        }

        public static Vector4 xzzz(this Vector3 a)
        {
            return new Vector4(a.x, a.z, a.z, a.z);
        }

        public static Vector4 yzzz(this Vector3 a)
        {
            return new Vector4(a.y, a.z, a.z, a.z);
        }

        public static Vector4 zzzz(this Vector3 a)
        {
            return new Vector4(a.z, a.z, a.z, a.z);
        }
    }
    internal static class Vector4Swizzles
    {
        //swizzles of size 2
        public static Vector2 _11(this Vector4 a)
        {
            return new Vector2(1.0f, 1.0f);
        }

        public static Vector2 _01(this Vector4 a)
        {
            return new Vector2(0.0f, 1.0f);
        }

        public static Vector2 x1(this Vector4 a)
        {
            return new Vector2(a.x, 1.0f);
        }

        public static Vector2 y1(this Vector4 a)
        {
            return new Vector2(a.y, 1.0f);
        }

        public static Vector2 z1(this Vector4 a)
        {
            return new Vector2(a.z, 1.0f);
        }

        public static Vector2 w1(this Vector4 a)
        {
            return new Vector2(a.w, 1.0f);
        }

        public static Vector2 _10(this Vector4 a)
        {
            return new Vector2(1.0f, 0.0f);
        }

        public static Vector2 _00(this Vector4 a)
        {
            return new Vector2(0.0f, 0.0f);
        }

        public static Vector2 x0(this Vector4 a)
        {
            return new Vector2(a.x, 0.0f);
        }

        public static Vector2 y0(this Vector4 a)
        {
            return new Vector2(a.y, 0.0f);
        }

        public static Vector2 z0(this Vector4 a)
        {
            return new Vector2(a.z, 0.0f);
        }

        public static Vector2 w0(this Vector4 a)
        {
            return new Vector2(a.w, 0.0f);
        }

        public static Vector2 _1x(this Vector4 a)
        {
            return new Vector2(1.0f, a.x);
        }

        public static Vector2 _0x(this Vector4 a)
        {
            return new Vector2(0.0f, a.x);
        }

        public static Vector2 xx(this Vector4 a)
        {
            return new Vector2(a.x, a.x);
        }

        public static Vector2 yx(this Vector4 a)
        {
            return new Vector2(a.y, a.x);
        }

        public static Vector2 zx(this Vector4 a)
        {
            return new Vector2(a.z, a.x);
        }

        public static Vector2 wx(this Vector4 a)
        {
            return new Vector2(a.w, a.x);
        }

        public static Vector2 _1y(this Vector4 a)
        {
            return new Vector2(1.0f, a.y);
        }

        public static Vector2 _0y(this Vector4 a)
        {
            return new Vector2(0.0f, a.y);
        }

        public static Vector2 xy(this Vector4 a)
        {
            return new Vector2(a.x, a.y);
        }

        public static Vector2 yy(this Vector4 a)
        {
            return new Vector2(a.y, a.y);
        }

        public static Vector2 zy(this Vector4 a)
        {
            return new Vector2(a.z, a.y);
        }

        public static Vector2 wy(this Vector4 a)
        {
            return new Vector2(a.w, a.y);
        }

        public static Vector2 _1z(this Vector4 a)
        {
            return new Vector2(1.0f, a.z);
        }

        public static Vector2 _0z(this Vector4 a)
        {
            return new Vector2(0.0f, a.z);
        }

        public static Vector2 xz(this Vector4 a)
        {
            return new Vector2(a.x, a.z);
        }

        public static Vector2 yz(this Vector4 a)
        {
            return new Vector2(a.y, a.z);
        }

        public static Vector2 zz(this Vector4 a)
        {
            return new Vector2(a.z, a.z);
        }

        public static Vector2 wz(this Vector4 a)
        {
            return new Vector2(a.w, a.z);
        }

        public static Vector2 _1w(this Vector4 a)
        {
            return new Vector2(1.0f, a.w);
        }

        public static Vector2 _0w(this Vector4 a)
        {
            return new Vector2(0.0f, a.w);
        }

        public static Vector2 xw(this Vector4 a)
        {
            return new Vector2(a.x, a.w);
        }

        public static Vector2 yw(this Vector4 a)
        {
            return new Vector2(a.y, a.w);
        }

        public static Vector2 zw(this Vector4 a)
        {
            return new Vector2(a.z, a.w);
        }

        public static Vector2 ww(this Vector4 a)
        {
            return new Vector2(a.w, a.w);
        }

        //swizzles of size 3
        public static Vector3 _111(this Vector4 a)
        {
            return new Vector3(1.0f, 1.0f, 1.0f);
        }

        public static Vector3 _011(this Vector4 a)
        {
            return new Vector3(0.0f, 1.0f, 1.0f);
        }

        public static Vector3 x11(this Vector4 a)
        {
            return new Vector3(a.x, 1.0f, 1.0f);
        }

        public static Vector3 y11(this Vector4 a)
        {
            return new Vector3(a.y, 1.0f, 1.0f);
        }

        public static Vector3 z11(this Vector4 a)
        {
            return new Vector3(a.z, 1.0f, 1.0f);
        }

        public static Vector3 w11(this Vector4 a)
        {
            return new Vector3(a.w, 1.0f, 1.0f);
        }

        public static Vector3 _101(this Vector4 a)
        {
            return new Vector3(1.0f, 0.0f, 1.0f);
        }

        public static Vector3 _001(this Vector4 a)
        {
            return new Vector3(0.0f, 0.0f, 1.0f);
        }

        public static Vector3 x01(this Vector4 a)
        {
            return new Vector3(a.x, 0.0f, 1.0f);
        }

        public static Vector3 y01(this Vector4 a)
        {
            return new Vector3(a.y, 0.0f, 1.0f);
        }

        public static Vector3 z01(this Vector4 a)
        {
            return new Vector3(a.z, 0.0f, 1.0f);
        }

        public static Vector3 w01(this Vector4 a)
        {
            return new Vector3(a.w, 0.0f, 1.0f);
        }

        public static Vector3 _1x1(this Vector4 a)
        {
            return new Vector3(1.0f, a.x, 1.0f);
        }

        public static Vector3 _0x1(this Vector4 a)
        {
            return new Vector3(0.0f, a.x, 1.0f);
        }

        public static Vector3 xx1(this Vector4 a)
        {
            return new Vector3(a.x, a.x, 1.0f);
        }

        public static Vector3 yx1(this Vector4 a)
        {
            return new Vector3(a.y, a.x, 1.0f);
        }

        public static Vector3 zx1(this Vector4 a)
        {
            return new Vector3(a.z, a.x, 1.0f);
        }

        public static Vector3 wx1(this Vector4 a)
        {
            return new Vector3(a.w, a.x, 1.0f);
        }

        public static Vector3 _1y1(this Vector4 a)
        {
            return new Vector3(1.0f, a.y, 1.0f);
        }

        public static Vector3 _0y1(this Vector4 a)
        {
            return new Vector3(0.0f, a.y, 1.0f);
        }

        public static Vector3 xy1(this Vector4 a)
        {
            return new Vector3(a.x, a.y, 1.0f);
        }

        public static Vector3 yy1(this Vector4 a)
        {
            return new Vector3(a.y, a.y, 1.0f);
        }

        public static Vector3 zy1(this Vector4 a)
        {
            return new Vector3(a.z, a.y, 1.0f);
        }

        public static Vector3 wy1(this Vector4 a)
        {
            return new Vector3(a.w, a.y, 1.0f);
        }

        public static Vector3 _1z1(this Vector4 a)
        {
            return new Vector3(1.0f, a.z, 1.0f);
        }

        public static Vector3 _0z1(this Vector4 a)
        {
            return new Vector3(0.0f, a.z, 1.0f);
        }

        public static Vector3 xz1(this Vector4 a)
        {
            return new Vector3(a.x, a.z, 1.0f);
        }

        public static Vector3 yz1(this Vector4 a)
        {
            return new Vector3(a.y, a.z, 1.0f);
        }

        public static Vector3 zz1(this Vector4 a)
        {
            return new Vector3(a.z, a.z, 1.0f);
        }

        public static Vector3 wz1(this Vector4 a)
        {
            return new Vector3(a.w, a.z, 1.0f);
        }

        public static Vector3 _1w1(this Vector4 a)
        {
            return new Vector3(1.0f, a.w, 1.0f);
        }

        public static Vector3 _0w1(this Vector4 a)
        {
            return new Vector3(0.0f, a.w, 1.0f);
        }

        public static Vector3 xw1(this Vector4 a)
        {
            return new Vector3(a.x, a.w, 1.0f);
        }

        public static Vector3 yw1(this Vector4 a)
        {
            return new Vector3(a.y, a.w, 1.0f);
        }

        public static Vector3 zw1(this Vector4 a)
        {
            return new Vector3(a.z, a.w, 1.0f);
        }

        public static Vector3 ww1(this Vector4 a)
        {
            return new Vector3(a.w, a.w, 1.0f);
        }

        public static Vector3 _110(this Vector4 a)
        {
            return new Vector3(1.0f, 1.0f, 0.0f);
        }

        public static Vector3 _010(this Vector4 a)
        {
            return new Vector3(0.0f, 1.0f, 0.0f);
        }

        public static Vector3 x10(this Vector4 a)
        {
            return new Vector3(a.x, 1.0f, 0.0f);
        }

        public static Vector3 y10(this Vector4 a)
        {
            return new Vector3(a.y, 1.0f, 0.0f);
        }

        public static Vector3 z10(this Vector4 a)
        {
            return new Vector3(a.z, 1.0f, 0.0f);
        }

        public static Vector3 w10(this Vector4 a)
        {
            return new Vector3(a.w, 1.0f, 0.0f);
        }

        public static Vector3 _100(this Vector4 a)
        {
            return new Vector3(1.0f, 0.0f, 0.0f);
        }

        public static Vector3 _000(this Vector4 a)
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }

        public static Vector3 x00(this Vector4 a)
        {
            return new Vector3(a.x, 0.0f, 0.0f);
        }

        public static Vector3 y00(this Vector4 a)
        {
            return new Vector3(a.y, 0.0f, 0.0f);
        }

        public static Vector3 z00(this Vector4 a)
        {
            return new Vector3(a.z, 0.0f, 0.0f);
        }

        public static Vector3 w00(this Vector4 a)
        {
            return new Vector3(a.w, 0.0f, 0.0f);
        }

        public static Vector3 _1x0(this Vector4 a)
        {
            return new Vector3(1.0f, a.x, 0.0f);
        }

        public static Vector3 _0x0(this Vector4 a)
        {
            return new Vector3(0.0f, a.x, 0.0f);
        }

        public static Vector3 xx0(this Vector4 a)
        {
            return new Vector3(a.x, a.x, 0.0f);
        }

        public static Vector3 yx0(this Vector4 a)
        {
            return new Vector3(a.y, a.x, 0.0f);
        }

        public static Vector3 zx0(this Vector4 a)
        {
            return new Vector3(a.z, a.x, 0.0f);
        }

        public static Vector3 wx0(this Vector4 a)
        {
            return new Vector3(a.w, a.x, 0.0f);
        }

        public static Vector3 _1y0(this Vector4 a)
        {
            return new Vector3(1.0f, a.y, 0.0f);
        }

        public static Vector3 _0y0(this Vector4 a)
        {
            return new Vector3(0.0f, a.y, 0.0f);
        }

        public static Vector3 xy0(this Vector4 a)
        {
            return new Vector3(a.x, a.y, 0.0f);
        }

        public static Vector3 yy0(this Vector4 a)
        {
            return new Vector3(a.y, a.y, 0.0f);
        }

        public static Vector3 zy0(this Vector4 a)
        {
            return new Vector3(a.z, a.y, 0.0f);
        }

        public static Vector3 wy0(this Vector4 a)
        {
            return new Vector3(a.w, a.y, 0.0f);
        }

        public static Vector3 _1z0(this Vector4 a)
        {
            return new Vector3(1.0f, a.z, 0.0f);
        }

        public static Vector3 _0z0(this Vector4 a)
        {
            return new Vector3(0.0f, a.z, 0.0f);
        }

        public static Vector3 xz0(this Vector4 a)
        {
            return new Vector3(a.x, a.z, 0.0f);
        }

        public static Vector3 yz0(this Vector4 a)
        {
            return new Vector3(a.y, a.z, 0.0f);
        }

        public static Vector3 zz0(this Vector4 a)
        {
            return new Vector3(a.z, a.z, 0.0f);
        }

        public static Vector3 wz0(this Vector4 a)
        {
            return new Vector3(a.w, a.z, 0.0f);
        }

        public static Vector3 _1w0(this Vector4 a)
        {
            return new Vector3(1.0f, a.w, 0.0f);
        }

        public static Vector3 _0w0(this Vector4 a)
        {
            return new Vector3(0.0f, a.w, 0.0f);
        }

        public static Vector3 xw0(this Vector4 a)
        {
            return new Vector3(a.x, a.w, 0.0f);
        }

        public static Vector3 yw0(this Vector4 a)
        {
            return new Vector3(a.y, a.w, 0.0f);
        }

        public static Vector3 zw0(this Vector4 a)
        {
            return new Vector3(a.z, a.w, 0.0f);
        }

        public static Vector3 ww0(this Vector4 a)
        {
            return new Vector3(a.w, a.w, 0.0f);
        }

        public static Vector3 _11x(this Vector4 a)
        {
            return new Vector3(1.0f, 1.0f, a.x);
        }

        public static Vector3 _01x(this Vector4 a)
        {
            return new Vector3(0.0f, 1.0f, a.x);
        }

        public static Vector3 x1x(this Vector4 a)
        {
            return new Vector3(a.x, 1.0f, a.x);
        }

        public static Vector3 y1x(this Vector4 a)
        {
            return new Vector3(a.y, 1.0f, a.x);
        }

        public static Vector3 z1x(this Vector4 a)
        {
            return new Vector3(a.z, 1.0f, a.x);
        }

        public static Vector3 w1x(this Vector4 a)
        {
            return new Vector3(a.w, 1.0f, a.x);
        }

        public static Vector3 _10x(this Vector4 a)
        {
            return new Vector3(1.0f, 0.0f, a.x);
        }

        public static Vector3 _00x(this Vector4 a)
        {
            return new Vector3(0.0f, 0.0f, a.x);
        }

        public static Vector3 x0x(this Vector4 a)
        {
            return new Vector3(a.x, 0.0f, a.x);
        }

        public static Vector3 y0x(this Vector4 a)
        {
            return new Vector3(a.y, 0.0f, a.x);
        }

        public static Vector3 z0x(this Vector4 a)
        {
            return new Vector3(a.z, 0.0f, a.x);
        }

        public static Vector3 w0x(this Vector4 a)
        {
            return new Vector3(a.w, 0.0f, a.x);
        }

        public static Vector3 _1xx(this Vector4 a)
        {
            return new Vector3(1.0f, a.x, a.x);
        }

        public static Vector3 _0xx(this Vector4 a)
        {
            return new Vector3(0.0f, a.x, a.x);
        }

        public static Vector3 xxx(this Vector4 a)
        {
            return new Vector3(a.x, a.x, a.x);
        }

        public static Vector3 yxx(this Vector4 a)
        {
            return new Vector3(a.y, a.x, a.x);
        }

        public static Vector3 zxx(this Vector4 a)
        {
            return new Vector3(a.z, a.x, a.x);
        }

        public static Vector3 wxx(this Vector4 a)
        {
            return new Vector3(a.w, a.x, a.x);
        }

        public static Vector3 _1yx(this Vector4 a)
        {
            return new Vector3(1.0f, a.y, a.x);
        }

        public static Vector3 _0yx(this Vector4 a)
        {
            return new Vector3(0.0f, a.y, a.x);
        }

        public static Vector3 xyx(this Vector4 a)
        {
            return new Vector3(a.x, a.y, a.x);
        }

        public static Vector3 yyx(this Vector4 a)
        {
            return new Vector3(a.y, a.y, a.x);
        }

        public static Vector3 zyx(this Vector4 a)
        {
            return new Vector3(a.z, a.y, a.x);
        }

        public static Vector3 wyx(this Vector4 a)
        {
            return new Vector3(a.w, a.y, a.x);
        }

        public static Vector3 _1zx(this Vector4 a)
        {
            return new Vector3(1.0f, a.z, a.x);
        }

        public static Vector3 _0zx(this Vector4 a)
        {
            return new Vector3(0.0f, a.z, a.x);
        }

        public static Vector3 xzx(this Vector4 a)
        {
            return new Vector3(a.x, a.z, a.x);
        }

        public static Vector3 yzx(this Vector4 a)
        {
            return new Vector3(a.y, a.z, a.x);
        }

        public static Vector3 zzx(this Vector4 a)
        {
            return new Vector3(a.z, a.z, a.x);
        }

        public static Vector3 wzx(this Vector4 a)
        {
            return new Vector3(a.w, a.z, a.x);
        }

        public static Vector3 _1wx(this Vector4 a)
        {
            return new Vector3(1.0f, a.w, a.x);
        }

        public static Vector3 _0wx(this Vector4 a)
        {
            return new Vector3(0.0f, a.w, a.x);
        }

        public static Vector3 xwx(this Vector4 a)
        {
            return new Vector3(a.x, a.w, a.x);
        }

        public static Vector3 ywx(this Vector4 a)
        {
            return new Vector3(a.y, a.w, a.x);
        }

        public static Vector3 zwx(this Vector4 a)
        {
            return new Vector3(a.z, a.w, a.x);
        }

        public static Vector3 wwx(this Vector4 a)
        {
            return new Vector3(a.w, a.w, a.x);
        }

        public static Vector3 _11y(this Vector4 a)
        {
            return new Vector3(1.0f, 1.0f, a.y);
        }

        public static Vector3 _01y(this Vector4 a)
        {
            return new Vector3(0.0f, 1.0f, a.y);
        }

        public static Vector3 x1y(this Vector4 a)
        {
            return new Vector3(a.x, 1.0f, a.y);
        }

        public static Vector3 y1y(this Vector4 a)
        {
            return new Vector3(a.y, 1.0f, a.y);
        }

        public static Vector3 z1y(this Vector4 a)
        {
            return new Vector3(a.z, 1.0f, a.y);
        }

        public static Vector3 w1y(this Vector4 a)
        {
            return new Vector3(a.w, 1.0f, a.y);
        }

        public static Vector3 _10y(this Vector4 a)
        {
            return new Vector3(1.0f, 0.0f, a.y);
        }

        public static Vector3 _00y(this Vector4 a)
        {
            return new Vector3(0.0f, 0.0f, a.y);
        }

        public static Vector3 x0y(this Vector4 a)
        {
            return new Vector3(a.x, 0.0f, a.y);
        }

        public static Vector3 y0y(this Vector4 a)
        {
            return new Vector3(a.y, 0.0f, a.y);
        }

        public static Vector3 z0y(this Vector4 a)
        {
            return new Vector3(a.z, 0.0f, a.y);
        }

        public static Vector3 w0y(this Vector4 a)
        {
            return new Vector3(a.w, 0.0f, a.y);
        }

        public static Vector3 _1xy(this Vector4 a)
        {
            return new Vector3(1.0f, a.x, a.y);
        }

        public static Vector3 _0xy(this Vector4 a)
        {
            return new Vector3(0.0f, a.x, a.y);
        }

        public static Vector3 xxy(this Vector4 a)
        {
            return new Vector3(a.x, a.x, a.y);
        }

        public static Vector3 yxy(this Vector4 a)
        {
            return new Vector3(a.y, a.x, a.y);
        }

        public static Vector3 zxy(this Vector4 a)
        {
            return new Vector3(a.z, a.x, a.y);
        }

        public static Vector3 wxy(this Vector4 a)
        {
            return new Vector3(a.w, a.x, a.y);
        }

        public static Vector3 _1yy(this Vector4 a)
        {
            return new Vector3(1.0f, a.y, a.y);
        }

        public static Vector3 _0yy(this Vector4 a)
        {
            return new Vector3(0.0f, a.y, a.y);
        }

        public static Vector3 xyy(this Vector4 a)
        {
            return new Vector3(a.x, a.y, a.y);
        }

        public static Vector3 yyy(this Vector4 a)
        {
            return new Vector3(a.y, a.y, a.y);
        }

        public static Vector3 zyy(this Vector4 a)
        {
            return new Vector3(a.z, a.y, a.y);
        }

        public static Vector3 wyy(this Vector4 a)
        {
            return new Vector3(a.w, a.y, a.y);
        }

        public static Vector3 _1zy(this Vector4 a)
        {
            return new Vector3(1.0f, a.z, a.y);
        }

        public static Vector3 _0zy(this Vector4 a)
        {
            return new Vector3(0.0f, a.z, a.y);
        }

        public static Vector3 xzy(this Vector4 a)
        {
            return new Vector3(a.x, a.z, a.y);
        }

        public static Vector3 yzy(this Vector4 a)
        {
            return new Vector3(a.y, a.z, a.y);
        }

        public static Vector3 zzy(this Vector4 a)
        {
            return new Vector3(a.z, a.z, a.y);
        }

        public static Vector3 wzy(this Vector4 a)
        {
            return new Vector3(a.w, a.z, a.y);
        }

        public static Vector3 _1wy(this Vector4 a)
        {
            return new Vector3(1.0f, a.w, a.y);
        }

        public static Vector3 _0wy(this Vector4 a)
        {
            return new Vector3(0.0f, a.w, a.y);
        }

        public static Vector3 xwy(this Vector4 a)
        {
            return new Vector3(a.x, a.w, a.y);
        }

        public static Vector3 ywy(this Vector4 a)
        {
            return new Vector3(a.y, a.w, a.y);
        }

        public static Vector3 zwy(this Vector4 a)
        {
            return new Vector3(a.z, a.w, a.y);
        }

        public static Vector3 wwy(this Vector4 a)
        {
            return new Vector3(a.w, a.w, a.y);
        }

        public static Vector3 _11z(this Vector4 a)
        {
            return new Vector3(1.0f, 1.0f, a.z);
        }

        public static Vector3 _01z(this Vector4 a)
        {
            return new Vector3(0.0f, 1.0f, a.z);
        }

        public static Vector3 x1z(this Vector4 a)
        {
            return new Vector3(a.x, 1.0f, a.z);
        }

        public static Vector3 y1z(this Vector4 a)
        {
            return new Vector3(a.y, 1.0f, a.z);
        }

        public static Vector3 z1z(this Vector4 a)
        {
            return new Vector3(a.z, 1.0f, a.z);
        }

        public static Vector3 w1z(this Vector4 a)
        {
            return new Vector3(a.w, 1.0f, a.z);
        }

        public static Vector3 _10z(this Vector4 a)
        {
            return new Vector3(1.0f, 0.0f, a.z);
        }

        public static Vector3 _00z(this Vector4 a)
        {
            return new Vector3(0.0f, 0.0f, a.z);
        }

        public static Vector3 x0z(this Vector4 a)
        {
            return new Vector3(a.x, 0.0f, a.z);
        }

        public static Vector3 y0z(this Vector4 a)
        {
            return new Vector3(a.y, 0.0f, a.z);
        }

        public static Vector3 z0z(this Vector4 a)
        {
            return new Vector3(a.z, 0.0f, a.z);
        }

        public static Vector3 w0z(this Vector4 a)
        {
            return new Vector3(a.w, 0.0f, a.z);
        }

        public static Vector3 _1xz(this Vector4 a)
        {
            return new Vector3(1.0f, a.x, a.z);
        }

        public static Vector3 _0xz(this Vector4 a)
        {
            return new Vector3(0.0f, a.x, a.z);
        }

        public static Vector3 xxz(this Vector4 a)
        {
            return new Vector3(a.x, a.x, a.z);
        }

        public static Vector3 yxz(this Vector4 a)
        {
            return new Vector3(a.y, a.x, a.z);
        }

        public static Vector3 zxz(this Vector4 a)
        {
            return new Vector3(a.z, a.x, a.z);
        }

        public static Vector3 wxz(this Vector4 a)
        {
            return new Vector3(a.w, a.x, a.z);
        }

        public static Vector3 _1yz(this Vector4 a)
        {
            return new Vector3(1.0f, a.y, a.z);
        }

        public static Vector3 _0yz(this Vector4 a)
        {
            return new Vector3(0.0f, a.y, a.z);
        }

        public static Vector3 xyz(this Vector4 a)
        {
            return new Vector3(a.x, a.y, a.z);
        }

        public static Vector3 yyz(this Vector4 a)
        {
            return new Vector3(a.y, a.y, a.z);
        }

        public static Vector3 zyz(this Vector4 a)
        {
            return new Vector3(a.z, a.y, a.z);
        }

        public static Vector3 wyz(this Vector4 a)
        {
            return new Vector3(a.w, a.y, a.z);
        }

        public static Vector3 _1zz(this Vector4 a)
        {
            return new Vector3(1.0f, a.z, a.z);
        }

        public static Vector3 _0zz(this Vector4 a)
        {
            return new Vector3(0.0f, a.z, a.z);
        }

        public static Vector3 xzz(this Vector4 a)
        {
            return new Vector3(a.x, a.z, a.z);
        }

        public static Vector3 yzz(this Vector4 a)
        {
            return new Vector3(a.y, a.z, a.z);
        }

        public static Vector3 zzz(this Vector4 a)
        {
            return new Vector3(a.z, a.z, a.z);
        }

        public static Vector3 wzz(this Vector4 a)
        {
            return new Vector3(a.w, a.z, a.z);
        }

        public static Vector3 _1wz(this Vector4 a)
        {
            return new Vector3(1.0f, a.w, a.z);
        }

        public static Vector3 _0wz(this Vector4 a)
        {
            return new Vector3(0.0f, a.w, a.z);
        }

        public static Vector3 xwz(this Vector4 a)
        {
            return new Vector3(a.x, a.w, a.z);
        }

        public static Vector3 ywz(this Vector4 a)
        {
            return new Vector3(a.y, a.w, a.z);
        }

        public static Vector3 zwz(this Vector4 a)
        {
            return new Vector3(a.z, a.w, a.z);
        }

        public static Vector3 wwz(this Vector4 a)
        {
            return new Vector3(a.w, a.w, a.z);
        }

        public static Vector3 _11w(this Vector4 a)
        {
            return new Vector3(1.0f, 1.0f, a.w);
        }

        public static Vector3 _01w(this Vector4 a)
        {
            return new Vector3(0.0f, 1.0f, a.w);
        }

        public static Vector3 x1w(this Vector4 a)
        {
            return new Vector3(a.x, 1.0f, a.w);
        }

        public static Vector3 y1w(this Vector4 a)
        {
            return new Vector3(a.y, 1.0f, a.w);
        }

        public static Vector3 z1w(this Vector4 a)
        {
            return new Vector3(a.z, 1.0f, a.w);
        }

        public static Vector3 w1w(this Vector4 a)
        {
            return new Vector3(a.w, 1.0f, a.w);
        }

        public static Vector3 _10w(this Vector4 a)
        {
            return new Vector3(1.0f, 0.0f, a.w);
        }

        public static Vector3 _00w(this Vector4 a)
        {
            return new Vector3(0.0f, 0.0f, a.w);
        }

        public static Vector3 x0w(this Vector4 a)
        {
            return new Vector3(a.x, 0.0f, a.w);
        }

        public static Vector3 y0w(this Vector4 a)
        {
            return new Vector3(a.y, 0.0f, a.w);
        }

        public static Vector3 z0w(this Vector4 a)
        {
            return new Vector3(a.z, 0.0f, a.w);
        }

        public static Vector3 w0w(this Vector4 a)
        {
            return new Vector3(a.w, 0.0f, a.w);
        }

        public static Vector3 _1xw(this Vector4 a)
        {
            return new Vector3(1.0f, a.x, a.w);
        }

        public static Vector3 _0xw(this Vector4 a)
        {
            return new Vector3(0.0f, a.x, a.w);
        }

        public static Vector3 xxw(this Vector4 a)
        {
            return new Vector3(a.x, a.x, a.w);
        }

        public static Vector3 yxw(this Vector4 a)
        {
            return new Vector3(a.y, a.x, a.w);
        }

        public static Vector3 zxw(this Vector4 a)
        {
            return new Vector3(a.z, a.x, a.w);
        }

        public static Vector3 wxw(this Vector4 a)
        {
            return new Vector3(a.w, a.x, a.w);
        }

        public static Vector3 _1yw(this Vector4 a)
        {
            return new Vector3(1.0f, a.y, a.w);
        }

        public static Vector3 _0yw(this Vector4 a)
        {
            return new Vector3(0.0f, a.y, a.w);
        }

        public static Vector3 xyw(this Vector4 a)
        {
            return new Vector3(a.x, a.y, a.w);
        }

        public static Vector3 yyw(this Vector4 a)
        {
            return new Vector3(a.y, a.y, a.w);
        }

        public static Vector3 zyw(this Vector4 a)
        {
            return new Vector3(a.z, a.y, a.w);
        }

        public static Vector3 wyw(this Vector4 a)
        {
            return new Vector3(a.w, a.y, a.w);
        }

        public static Vector3 _1zw(this Vector4 a)
        {
            return new Vector3(1.0f, a.z, a.w);
        }

        public static Vector3 _0zw(this Vector4 a)
        {
            return new Vector3(0.0f, a.z, a.w);
        }

        public static Vector3 xzw(this Vector4 a)
        {
            return new Vector3(a.x, a.z, a.w);
        }

        public static Vector3 yzw(this Vector4 a)
        {
            return new Vector3(a.y, a.z, a.w);
        }

        public static Vector3 zzw(this Vector4 a)
        {
            return new Vector3(a.z, a.z, a.w);
        }

        public static Vector3 wzw(this Vector4 a)
        {
            return new Vector3(a.w, a.z, a.w);
        }

        public static Vector3 _1ww(this Vector4 a)
        {
            return new Vector3(1.0f, a.w, a.w);
        }

        public static Vector3 _0ww(this Vector4 a)
        {
            return new Vector3(0.0f, a.w, a.w);
        }

        public static Vector3 xww(this Vector4 a)
        {
            return new Vector3(a.x, a.w, a.w);
        }

        public static Vector3 yww(this Vector4 a)
        {
            return new Vector3(a.y, a.w, a.w);
        }

        public static Vector3 zww(this Vector4 a)
        {
            return new Vector3(a.z, a.w, a.w);
        }

        public static Vector3 www(this Vector4 a)
        {
            return new Vector3(a.w, a.w, a.w);
        }

        //swizzles of size 4
        public static Vector4 _1111(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 _0111(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 x111(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 y111(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 z111(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 w111(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 1.0f, 1.0f);
        }

        public static Vector4 _1011(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 _0011(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 x011(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 y011(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 z011(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 w011(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 1.0f, 1.0f);
        }

        public static Vector4 _1x11(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, 1.0f);
        }

        public static Vector4 _0x11(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, 1.0f);
        }

        public static Vector4 xx11(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 1.0f, 1.0f);
        }

        public static Vector4 yx11(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 1.0f, 1.0f);
        }

        public static Vector4 zx11(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 1.0f, 1.0f);
        }

        public static Vector4 wx11(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 1.0f, 1.0f);
        }

        public static Vector4 _1y11(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, 1.0f);
        }

        public static Vector4 _0y11(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, 1.0f);
        }

        public static Vector4 xy11(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 1.0f, 1.0f);
        }

        public static Vector4 yy11(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 1.0f, 1.0f);
        }

        public static Vector4 zy11(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 1.0f, 1.0f);
        }

        public static Vector4 wy11(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 1.0f, 1.0f);
        }

        public static Vector4 _1z11(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, 1.0f);
        }

        public static Vector4 _0z11(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, 1.0f);
        }

        public static Vector4 xz11(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 1.0f, 1.0f);
        }

        public static Vector4 yz11(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 1.0f, 1.0f);
        }

        public static Vector4 zz11(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 1.0f, 1.0f);
        }

        public static Vector4 wz11(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 1.0f, 1.0f);
        }

        public static Vector4 _1w11(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 1.0f, 1.0f);
        }

        public static Vector4 _0w11(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 1.0f, 1.0f);
        }

        public static Vector4 xw11(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 1.0f, 1.0f);
        }

        public static Vector4 yw11(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 1.0f, 1.0f);
        }

        public static Vector4 zw11(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 1.0f, 1.0f);
        }

        public static Vector4 ww11(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 1.0f, 1.0f);
        }

        public static Vector4 _1101(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 _0101(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 x101(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 y101(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 z101(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 w101(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 0.0f, 1.0f);
        }

        public static Vector4 _1001(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 _0001(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 x001(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 y001(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 z001(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 w001(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 0.0f, 1.0f);
        }

        public static Vector4 _1x01(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, 1.0f);
        }

        public static Vector4 _0x01(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, 1.0f);
        }

        public static Vector4 xx01(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 0.0f, 1.0f);
        }

        public static Vector4 yx01(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 0.0f, 1.0f);
        }

        public static Vector4 zx01(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 0.0f, 1.0f);
        }

        public static Vector4 wx01(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 0.0f, 1.0f);
        }

        public static Vector4 _1y01(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, 1.0f);
        }

        public static Vector4 _0y01(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, 1.0f);
        }

        public static Vector4 xy01(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 0.0f, 1.0f);
        }

        public static Vector4 yy01(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 0.0f, 1.0f);
        }

        public static Vector4 zy01(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 0.0f, 1.0f);
        }

        public static Vector4 wy01(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 0.0f, 1.0f);
        }

        public static Vector4 _1z01(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, 1.0f);
        }

        public static Vector4 _0z01(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, 1.0f);
        }

        public static Vector4 xz01(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 0.0f, 1.0f);
        }

        public static Vector4 yz01(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 0.0f, 1.0f);
        }

        public static Vector4 zz01(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 0.0f, 1.0f);
        }

        public static Vector4 wz01(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 0.0f, 1.0f);
        }

        public static Vector4 _1w01(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 0.0f, 1.0f);
        }

        public static Vector4 _0w01(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 0.0f, 1.0f);
        }

        public static Vector4 xw01(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 0.0f, 1.0f);
        }

        public static Vector4 yw01(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 0.0f, 1.0f);
        }

        public static Vector4 zw01(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 0.0f, 1.0f);
        }

        public static Vector4 ww01(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 0.0f, 1.0f);
        }

        public static Vector4 _11x1(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, 1.0f);
        }

        public static Vector4 _01x1(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, 1.0f);
        }

        public static Vector4 x1x1(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.x, 1.0f);
        }

        public static Vector4 y1x1(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.x, 1.0f);
        }

        public static Vector4 z1x1(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.x, 1.0f);
        }

        public static Vector4 w1x1(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.x, 1.0f);
        }

        public static Vector4 _10x1(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, 1.0f);
        }

        public static Vector4 _00x1(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, 1.0f);
        }

        public static Vector4 x0x1(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.x, 1.0f);
        }

        public static Vector4 y0x1(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.x, 1.0f);
        }

        public static Vector4 z0x1(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.x, 1.0f);
        }

        public static Vector4 w0x1(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.x, 1.0f);
        }

        public static Vector4 _1xx1(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.x, 1.0f);
        }

        public static Vector4 _0xx1(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.x, 1.0f);
        }

        public static Vector4 xxx1(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.x, 1.0f);
        }

        public static Vector4 yxx1(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.x, 1.0f);
        }

        public static Vector4 zxx1(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.x, 1.0f);
        }

        public static Vector4 wxx1(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.x, 1.0f);
        }

        public static Vector4 _1yx1(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.x, 1.0f);
        }

        public static Vector4 _0yx1(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.x, 1.0f);
        }

        public static Vector4 xyx1(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.x, 1.0f);
        }

        public static Vector4 yyx1(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.x, 1.0f);
        }

        public static Vector4 zyx1(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.x, 1.0f);
        }

        public static Vector4 wyx1(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.x, 1.0f);
        }

        public static Vector4 _1zx1(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.x, 1.0f);
        }

        public static Vector4 _0zx1(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.x, 1.0f);
        }

        public static Vector4 xzx1(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.x, 1.0f);
        }

        public static Vector4 yzx1(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.x, 1.0f);
        }

        public static Vector4 zzx1(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.x, 1.0f);
        }

        public static Vector4 wzx1(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.x, 1.0f);
        }

        public static Vector4 _1wx1(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.x, 1.0f);
        }

        public static Vector4 _0wx1(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.x, 1.0f);
        }

        public static Vector4 xwx1(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.x, 1.0f);
        }

        public static Vector4 ywx1(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.x, 1.0f);
        }

        public static Vector4 zwx1(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.x, 1.0f);
        }

        public static Vector4 wwx1(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.x, 1.0f);
        }

        public static Vector4 _11y1(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, 1.0f);
        }

        public static Vector4 _01y1(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, 1.0f);
        }

        public static Vector4 x1y1(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.y, 1.0f);
        }

        public static Vector4 y1y1(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.y, 1.0f);
        }

        public static Vector4 z1y1(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.y, 1.0f);
        }

        public static Vector4 w1y1(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.y, 1.0f);
        }

        public static Vector4 _10y1(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, 1.0f);
        }

        public static Vector4 _00y1(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, 1.0f);
        }

        public static Vector4 x0y1(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.y, 1.0f);
        }

        public static Vector4 y0y1(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.y, 1.0f);
        }

        public static Vector4 z0y1(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.y, 1.0f);
        }

        public static Vector4 w0y1(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.y, 1.0f);
        }

        public static Vector4 _1xy1(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.y, 1.0f);
        }

        public static Vector4 _0xy1(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.y, 1.0f);
        }

        public static Vector4 xxy1(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.y, 1.0f);
        }

        public static Vector4 yxy1(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.y, 1.0f);
        }

        public static Vector4 zxy1(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.y, 1.0f);
        }

        public static Vector4 wxy1(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.y, 1.0f);
        }

        public static Vector4 _1yy1(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.y, 1.0f);
        }

        public static Vector4 _0yy1(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.y, 1.0f);
        }

        public static Vector4 xyy1(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.y, 1.0f);
        }

        public static Vector4 yyy1(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.y, 1.0f);
        }

        public static Vector4 zyy1(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.y, 1.0f);
        }

        public static Vector4 wyy1(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.y, 1.0f);
        }

        public static Vector4 _1zy1(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.y, 1.0f);
        }

        public static Vector4 _0zy1(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.y, 1.0f);
        }

        public static Vector4 xzy1(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.y, 1.0f);
        }

        public static Vector4 yzy1(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.y, 1.0f);
        }

        public static Vector4 zzy1(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.y, 1.0f);
        }

        public static Vector4 wzy1(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.y, 1.0f);
        }

        public static Vector4 _1wy1(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.y, 1.0f);
        }

        public static Vector4 _0wy1(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.y, 1.0f);
        }

        public static Vector4 xwy1(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.y, 1.0f);
        }

        public static Vector4 ywy1(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.y, 1.0f);
        }

        public static Vector4 zwy1(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.y, 1.0f);
        }

        public static Vector4 wwy1(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.y, 1.0f);
        }

        public static Vector4 _11z1(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, 1.0f);
        }

        public static Vector4 _01z1(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, 1.0f);
        }

        public static Vector4 x1z1(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.z, 1.0f);
        }

        public static Vector4 y1z1(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.z, 1.0f);
        }

        public static Vector4 z1z1(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.z, 1.0f);
        }

        public static Vector4 w1z1(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.z, 1.0f);
        }

        public static Vector4 _10z1(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, 1.0f);
        }

        public static Vector4 _00z1(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, 1.0f);
        }

        public static Vector4 x0z1(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.z, 1.0f);
        }

        public static Vector4 y0z1(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.z, 1.0f);
        }

        public static Vector4 z0z1(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.z, 1.0f);
        }

        public static Vector4 w0z1(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.z, 1.0f);
        }

        public static Vector4 _1xz1(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.z, 1.0f);
        }

        public static Vector4 _0xz1(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.z, 1.0f);
        }

        public static Vector4 xxz1(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.z, 1.0f);
        }

        public static Vector4 yxz1(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.z, 1.0f);
        }

        public static Vector4 zxz1(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.z, 1.0f);
        }

        public static Vector4 wxz1(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.z, 1.0f);
        }

        public static Vector4 _1yz1(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.z, 1.0f);
        }

        public static Vector4 _0yz1(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.z, 1.0f);
        }

        public static Vector4 xyz1(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.z, 1.0f);
        }

        public static Vector4 yyz1(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.z, 1.0f);
        }

        public static Vector4 zyz1(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.z, 1.0f);
        }

        public static Vector4 wyz1(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.z, 1.0f);
        }

        public static Vector4 _1zz1(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.z, 1.0f);
        }

        public static Vector4 _0zz1(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.z, 1.0f);
        }

        public static Vector4 xzz1(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.z, 1.0f);
        }

        public static Vector4 yzz1(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.z, 1.0f);
        }

        public static Vector4 zzz1(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.z, 1.0f);
        }

        public static Vector4 wzz1(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.z, 1.0f);
        }

        public static Vector4 _1wz1(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.z, 1.0f);
        }

        public static Vector4 _0wz1(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.z, 1.0f);
        }

        public static Vector4 xwz1(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.z, 1.0f);
        }

        public static Vector4 ywz1(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.z, 1.0f);
        }

        public static Vector4 zwz1(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.z, 1.0f);
        }

        public static Vector4 wwz1(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.z, 1.0f);
        }

        public static Vector4 _11w1(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.w, 1.0f);
        }

        public static Vector4 _01w1(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.w, 1.0f);
        }

        public static Vector4 x1w1(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.w, 1.0f);
        }

        public static Vector4 y1w1(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.w, 1.0f);
        }

        public static Vector4 z1w1(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.w, 1.0f);
        }

        public static Vector4 w1w1(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.w, 1.0f);
        }

        public static Vector4 _10w1(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.w, 1.0f);
        }

        public static Vector4 _00w1(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.w, 1.0f);
        }

        public static Vector4 x0w1(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.w, 1.0f);
        }

        public static Vector4 y0w1(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.w, 1.0f);
        }

        public static Vector4 z0w1(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.w, 1.0f);
        }

        public static Vector4 w0w1(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.w, 1.0f);
        }

        public static Vector4 _1xw1(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.w, 1.0f);
        }

        public static Vector4 _0xw1(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.w, 1.0f);
        }

        public static Vector4 xxw1(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.w, 1.0f);
        }

        public static Vector4 yxw1(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.w, 1.0f);
        }

        public static Vector4 zxw1(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.w, 1.0f);
        }

        public static Vector4 wxw1(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.w, 1.0f);
        }

        public static Vector4 _1yw1(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.w, 1.0f);
        }

        public static Vector4 _0yw1(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.w, 1.0f);
        }

        public static Vector4 xyw1(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.w, 1.0f);
        }

        public static Vector4 yyw1(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.w, 1.0f);
        }

        public static Vector4 zyw1(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.w, 1.0f);
        }

        public static Vector4 wyw1(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.w, 1.0f);
        }

        public static Vector4 _1zw1(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.w, 1.0f);
        }

        public static Vector4 _0zw1(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.w, 1.0f);
        }

        public static Vector4 xzw1(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.w, 1.0f);
        }

        public static Vector4 yzw1(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.w, 1.0f);
        }

        public static Vector4 zzw1(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.w, 1.0f);
        }

        public static Vector4 wzw1(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.w, 1.0f);
        }

        public static Vector4 _1ww1(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.w, 1.0f);
        }

        public static Vector4 _0ww1(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.w, 1.0f);
        }

        public static Vector4 xww1(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.w, 1.0f);
        }

        public static Vector4 yww1(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.w, 1.0f);
        }

        public static Vector4 zww1(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.w, 1.0f);
        }

        public static Vector4 www1(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.w, 1.0f);
        }

        public static Vector4 _1110(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 _0110(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 x110(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 y110(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 z110(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 w110(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 1.0f, 0.0f);
        }

        public static Vector4 _1010(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 _0010(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 x010(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 y010(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 z010(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 w010(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 1.0f, 0.0f);
        }

        public static Vector4 _1x10(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, 0.0f);
        }

        public static Vector4 _0x10(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, 0.0f);
        }

        public static Vector4 xx10(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 1.0f, 0.0f);
        }

        public static Vector4 yx10(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 1.0f, 0.0f);
        }

        public static Vector4 zx10(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 1.0f, 0.0f);
        }

        public static Vector4 wx10(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 1.0f, 0.0f);
        }

        public static Vector4 _1y10(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, 0.0f);
        }

        public static Vector4 _0y10(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, 0.0f);
        }

        public static Vector4 xy10(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 1.0f, 0.0f);
        }

        public static Vector4 yy10(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 1.0f, 0.0f);
        }

        public static Vector4 zy10(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 1.0f, 0.0f);
        }

        public static Vector4 wy10(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 1.0f, 0.0f);
        }

        public static Vector4 _1z10(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, 0.0f);
        }

        public static Vector4 _0z10(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, 0.0f);
        }

        public static Vector4 xz10(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 1.0f, 0.0f);
        }

        public static Vector4 yz10(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 1.0f, 0.0f);
        }

        public static Vector4 zz10(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 1.0f, 0.0f);
        }

        public static Vector4 wz10(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 1.0f, 0.0f);
        }

        public static Vector4 _1w10(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 1.0f, 0.0f);
        }

        public static Vector4 _0w10(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 1.0f, 0.0f);
        }

        public static Vector4 xw10(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 1.0f, 0.0f);
        }

        public static Vector4 yw10(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 1.0f, 0.0f);
        }

        public static Vector4 zw10(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 1.0f, 0.0f);
        }

        public static Vector4 ww10(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 1.0f, 0.0f);
        }

        public static Vector4 _1100(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 _0100(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 x100(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 y100(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 z100(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 w100(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 0.0f, 0.0f);
        }

        public static Vector4 _1000(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 _0000(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 x000(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 y000(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 z000(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 w000(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 0.0f, 0.0f);
        }

        public static Vector4 _1x00(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, 0.0f);
        }

        public static Vector4 _0x00(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, 0.0f);
        }

        public static Vector4 xx00(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 0.0f, 0.0f);
        }

        public static Vector4 yx00(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 0.0f, 0.0f);
        }

        public static Vector4 zx00(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 0.0f, 0.0f);
        }

        public static Vector4 wx00(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 0.0f, 0.0f);
        }

        public static Vector4 _1y00(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, 0.0f);
        }

        public static Vector4 _0y00(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, 0.0f);
        }

        public static Vector4 xy00(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 0.0f, 0.0f);
        }

        public static Vector4 yy00(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 0.0f, 0.0f);
        }

        public static Vector4 zy00(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 0.0f, 0.0f);
        }

        public static Vector4 wy00(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 0.0f, 0.0f);
        }

        public static Vector4 _1z00(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, 0.0f);
        }

        public static Vector4 _0z00(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, 0.0f);
        }

        public static Vector4 xz00(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 0.0f, 0.0f);
        }

        public static Vector4 yz00(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 0.0f, 0.0f);
        }

        public static Vector4 zz00(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 0.0f, 0.0f);
        }

        public static Vector4 wz00(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 0.0f, 0.0f);
        }

        public static Vector4 _1w00(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 0.0f, 0.0f);
        }

        public static Vector4 _0w00(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 0.0f, 0.0f);
        }

        public static Vector4 xw00(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 0.0f, 0.0f);
        }

        public static Vector4 yw00(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 0.0f, 0.0f);
        }

        public static Vector4 zw00(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 0.0f, 0.0f);
        }

        public static Vector4 ww00(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 0.0f, 0.0f);
        }

        public static Vector4 _11x0(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, 0.0f);
        }

        public static Vector4 _01x0(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, 0.0f);
        }

        public static Vector4 x1x0(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.x, 0.0f);
        }

        public static Vector4 y1x0(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.x, 0.0f);
        }

        public static Vector4 z1x0(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.x, 0.0f);
        }

        public static Vector4 w1x0(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.x, 0.0f);
        }

        public static Vector4 _10x0(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, 0.0f);
        }

        public static Vector4 _00x0(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, 0.0f);
        }

        public static Vector4 x0x0(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.x, 0.0f);
        }

        public static Vector4 y0x0(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.x, 0.0f);
        }

        public static Vector4 z0x0(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.x, 0.0f);
        }

        public static Vector4 w0x0(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.x, 0.0f);
        }

        public static Vector4 _1xx0(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.x, 0.0f);
        }

        public static Vector4 _0xx0(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.x, 0.0f);
        }

        public static Vector4 xxx0(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.x, 0.0f);
        }

        public static Vector4 yxx0(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.x, 0.0f);
        }

        public static Vector4 zxx0(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.x, 0.0f);
        }

        public static Vector4 wxx0(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.x, 0.0f);
        }

        public static Vector4 _1yx0(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.x, 0.0f);
        }

        public static Vector4 _0yx0(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.x, 0.0f);
        }

        public static Vector4 xyx0(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.x, 0.0f);
        }

        public static Vector4 yyx0(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.x, 0.0f);
        }

        public static Vector4 zyx0(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.x, 0.0f);
        }

        public static Vector4 wyx0(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.x, 0.0f);
        }

        public static Vector4 _1zx0(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.x, 0.0f);
        }

        public static Vector4 _0zx0(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.x, 0.0f);
        }

        public static Vector4 xzx0(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.x, 0.0f);
        }

        public static Vector4 yzx0(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.x, 0.0f);
        }

        public static Vector4 zzx0(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.x, 0.0f);
        }

        public static Vector4 wzx0(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.x, 0.0f);
        }

        public static Vector4 _1wx0(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.x, 0.0f);
        }

        public static Vector4 _0wx0(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.x, 0.0f);
        }

        public static Vector4 xwx0(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.x, 0.0f);
        }

        public static Vector4 ywx0(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.x, 0.0f);
        }

        public static Vector4 zwx0(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.x, 0.0f);
        }

        public static Vector4 wwx0(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.x, 0.0f);
        }

        public static Vector4 _11y0(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, 0.0f);
        }

        public static Vector4 _01y0(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, 0.0f);
        }

        public static Vector4 x1y0(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.y, 0.0f);
        }

        public static Vector4 y1y0(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.y, 0.0f);
        }

        public static Vector4 z1y0(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.y, 0.0f);
        }

        public static Vector4 w1y0(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.y, 0.0f);
        }

        public static Vector4 _10y0(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, 0.0f);
        }

        public static Vector4 _00y0(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, 0.0f);
        }

        public static Vector4 x0y0(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.y, 0.0f);
        }

        public static Vector4 y0y0(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.y, 0.0f);
        }

        public static Vector4 z0y0(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.y, 0.0f);
        }

        public static Vector4 w0y0(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.y, 0.0f);
        }

        public static Vector4 _1xy0(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.y, 0.0f);
        }

        public static Vector4 _0xy0(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.y, 0.0f);
        }

        public static Vector4 xxy0(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.y, 0.0f);
        }

        public static Vector4 yxy0(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.y, 0.0f);
        }

        public static Vector4 zxy0(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.y, 0.0f);
        }

        public static Vector4 wxy0(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.y, 0.0f);
        }

        public static Vector4 _1yy0(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.y, 0.0f);
        }

        public static Vector4 _0yy0(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.y, 0.0f);
        }

        public static Vector4 xyy0(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.y, 0.0f);
        }

        public static Vector4 yyy0(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.y, 0.0f);
        }

        public static Vector4 zyy0(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.y, 0.0f);
        }

        public static Vector4 wyy0(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.y, 0.0f);
        }

        public static Vector4 _1zy0(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.y, 0.0f);
        }

        public static Vector4 _0zy0(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.y, 0.0f);
        }

        public static Vector4 xzy0(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.y, 0.0f);
        }

        public static Vector4 yzy0(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.y, 0.0f);
        }

        public static Vector4 zzy0(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.y, 0.0f);
        }

        public static Vector4 wzy0(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.y, 0.0f);
        }

        public static Vector4 _1wy0(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.y, 0.0f);
        }

        public static Vector4 _0wy0(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.y, 0.0f);
        }

        public static Vector4 xwy0(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.y, 0.0f);
        }

        public static Vector4 ywy0(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.y, 0.0f);
        }

        public static Vector4 zwy0(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.y, 0.0f);
        }

        public static Vector4 wwy0(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.y, 0.0f);
        }

        public static Vector4 _11z0(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, 0.0f);
        }

        public static Vector4 _01z0(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, 0.0f);
        }

        public static Vector4 x1z0(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.z, 0.0f);
        }

        public static Vector4 y1z0(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.z, 0.0f);
        }

        public static Vector4 z1z0(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.z, 0.0f);
        }

        public static Vector4 w1z0(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.z, 0.0f);
        }

        public static Vector4 _10z0(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, 0.0f);
        }

        public static Vector4 _00z0(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, 0.0f);
        }

        public static Vector4 x0z0(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.z, 0.0f);
        }

        public static Vector4 y0z0(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.z, 0.0f);
        }

        public static Vector4 z0z0(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.z, 0.0f);
        }

        public static Vector4 w0z0(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.z, 0.0f);
        }

        public static Vector4 _1xz0(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.z, 0.0f);
        }

        public static Vector4 _0xz0(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.z, 0.0f);
        }

        public static Vector4 xxz0(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.z, 0.0f);
        }

        public static Vector4 yxz0(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.z, 0.0f);
        }

        public static Vector4 zxz0(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.z, 0.0f);
        }

        public static Vector4 wxz0(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.z, 0.0f);
        }

        public static Vector4 _1yz0(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.z, 0.0f);
        }

        public static Vector4 _0yz0(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.z, 0.0f);
        }

        public static Vector4 xyz0(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.z, 0.0f);
        }

        public static Vector4 yyz0(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.z, 0.0f);
        }

        public static Vector4 zyz0(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.z, 0.0f);
        }

        public static Vector4 wyz0(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.z, 0.0f);
        }

        public static Vector4 _1zz0(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.z, 0.0f);
        }

        public static Vector4 _0zz0(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.z, 0.0f);
        }

        public static Vector4 xzz0(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.z, 0.0f);
        }

        public static Vector4 yzz0(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.z, 0.0f);
        }

        public static Vector4 zzz0(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.z, 0.0f);
        }

        public static Vector4 wzz0(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.z, 0.0f);
        }

        public static Vector4 _1wz0(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.z, 0.0f);
        }

        public static Vector4 _0wz0(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.z, 0.0f);
        }

        public static Vector4 xwz0(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.z, 0.0f);
        }

        public static Vector4 ywz0(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.z, 0.0f);
        }

        public static Vector4 zwz0(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.z, 0.0f);
        }

        public static Vector4 wwz0(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.z, 0.0f);
        }

        public static Vector4 _11w0(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.w, 0.0f);
        }

        public static Vector4 _01w0(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.w, 0.0f);
        }

        public static Vector4 x1w0(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.w, 0.0f);
        }

        public static Vector4 y1w0(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.w, 0.0f);
        }

        public static Vector4 z1w0(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.w, 0.0f);
        }

        public static Vector4 w1w0(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.w, 0.0f);
        }

        public static Vector4 _10w0(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.w, 0.0f);
        }

        public static Vector4 _00w0(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.w, 0.0f);
        }

        public static Vector4 x0w0(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.w, 0.0f);
        }

        public static Vector4 y0w0(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.w, 0.0f);
        }

        public static Vector4 z0w0(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.w, 0.0f);
        }

        public static Vector4 w0w0(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.w, 0.0f);
        }

        public static Vector4 _1xw0(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.w, 0.0f);
        }

        public static Vector4 _0xw0(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.w, 0.0f);
        }

        public static Vector4 xxw0(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.w, 0.0f);
        }

        public static Vector4 yxw0(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.w, 0.0f);
        }

        public static Vector4 zxw0(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.w, 0.0f);
        }

        public static Vector4 wxw0(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.w, 0.0f);
        }

        public static Vector4 _1yw0(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.w, 0.0f);
        }

        public static Vector4 _0yw0(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.w, 0.0f);
        }

        public static Vector4 xyw0(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.w, 0.0f);
        }

        public static Vector4 yyw0(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.w, 0.0f);
        }

        public static Vector4 zyw0(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.w, 0.0f);
        }

        public static Vector4 wyw0(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.w, 0.0f);
        }

        public static Vector4 _1zw0(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.w, 0.0f);
        }

        public static Vector4 _0zw0(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.w, 0.0f);
        }

        public static Vector4 xzw0(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.w, 0.0f);
        }

        public static Vector4 yzw0(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.w, 0.0f);
        }

        public static Vector4 zzw0(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.w, 0.0f);
        }

        public static Vector4 wzw0(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.w, 0.0f);
        }

        public static Vector4 _1ww0(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.w, 0.0f);
        }

        public static Vector4 _0ww0(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.w, 0.0f);
        }

        public static Vector4 xww0(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.w, 0.0f);
        }

        public static Vector4 yww0(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.w, 0.0f);
        }

        public static Vector4 zww0(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.w, 0.0f);
        }

        public static Vector4 www0(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.w, 0.0f);
        }

        public static Vector4 _111x(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, a.x);
        }

        public static Vector4 _011x(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, a.x);
        }

        public static Vector4 x11x(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, a.x);
        }

        public static Vector4 y11x(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, a.x);
        }

        public static Vector4 z11x(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, a.x);
        }

        public static Vector4 w11x(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 1.0f, a.x);
        }

        public static Vector4 _101x(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, a.x);
        }

        public static Vector4 _001x(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, a.x);
        }

        public static Vector4 x01x(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, a.x);
        }

        public static Vector4 y01x(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, a.x);
        }

        public static Vector4 z01x(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, a.x);
        }

        public static Vector4 w01x(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 1.0f, a.x);
        }

        public static Vector4 _1x1x(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, a.x);
        }

        public static Vector4 _0x1x(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, a.x);
        }

        public static Vector4 xx1x(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 1.0f, a.x);
        }

        public static Vector4 yx1x(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 1.0f, a.x);
        }

        public static Vector4 zx1x(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 1.0f, a.x);
        }

        public static Vector4 wx1x(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 1.0f, a.x);
        }

        public static Vector4 _1y1x(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, a.x);
        }

        public static Vector4 _0y1x(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, a.x);
        }

        public static Vector4 xy1x(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 1.0f, a.x);
        }

        public static Vector4 yy1x(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 1.0f, a.x);
        }

        public static Vector4 zy1x(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 1.0f, a.x);
        }

        public static Vector4 wy1x(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 1.0f, a.x);
        }

        public static Vector4 _1z1x(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, a.x);
        }

        public static Vector4 _0z1x(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, a.x);
        }

        public static Vector4 xz1x(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 1.0f, a.x);
        }

        public static Vector4 yz1x(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 1.0f, a.x);
        }

        public static Vector4 zz1x(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 1.0f, a.x);
        }

        public static Vector4 wz1x(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 1.0f, a.x);
        }

        public static Vector4 _1w1x(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 1.0f, a.x);
        }

        public static Vector4 _0w1x(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 1.0f, a.x);
        }

        public static Vector4 xw1x(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 1.0f, a.x);
        }

        public static Vector4 yw1x(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 1.0f, a.x);
        }

        public static Vector4 zw1x(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 1.0f, a.x);
        }

        public static Vector4 ww1x(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 1.0f, a.x);
        }

        public static Vector4 _110x(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, a.x);
        }

        public static Vector4 _010x(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, a.x);
        }

        public static Vector4 x10x(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, a.x);
        }

        public static Vector4 y10x(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, a.x);
        }

        public static Vector4 z10x(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, a.x);
        }

        public static Vector4 w10x(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 0.0f, a.x);
        }

        public static Vector4 _100x(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, a.x);
        }

        public static Vector4 _000x(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, a.x);
        }

        public static Vector4 x00x(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, a.x);
        }

        public static Vector4 y00x(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, a.x);
        }

        public static Vector4 z00x(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, a.x);
        }

        public static Vector4 w00x(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 0.0f, a.x);
        }

        public static Vector4 _1x0x(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, a.x);
        }

        public static Vector4 _0x0x(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, a.x);
        }

        public static Vector4 xx0x(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 0.0f, a.x);
        }

        public static Vector4 yx0x(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 0.0f, a.x);
        }

        public static Vector4 zx0x(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 0.0f, a.x);
        }

        public static Vector4 wx0x(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 0.0f, a.x);
        }

        public static Vector4 _1y0x(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, a.x);
        }

        public static Vector4 _0y0x(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, a.x);
        }

        public static Vector4 xy0x(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 0.0f, a.x);
        }

        public static Vector4 yy0x(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 0.0f, a.x);
        }

        public static Vector4 zy0x(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 0.0f, a.x);
        }

        public static Vector4 wy0x(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 0.0f, a.x);
        }

        public static Vector4 _1z0x(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, a.x);
        }

        public static Vector4 _0z0x(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, a.x);
        }

        public static Vector4 xz0x(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 0.0f, a.x);
        }

        public static Vector4 yz0x(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 0.0f, a.x);
        }

        public static Vector4 zz0x(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 0.0f, a.x);
        }

        public static Vector4 wz0x(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 0.0f, a.x);
        }

        public static Vector4 _1w0x(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 0.0f, a.x);
        }

        public static Vector4 _0w0x(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 0.0f, a.x);
        }

        public static Vector4 xw0x(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 0.0f, a.x);
        }

        public static Vector4 yw0x(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 0.0f, a.x);
        }

        public static Vector4 zw0x(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 0.0f, a.x);
        }

        public static Vector4 ww0x(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 0.0f, a.x);
        }

        public static Vector4 _11xx(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, a.x);
        }

        public static Vector4 _01xx(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, a.x);
        }

        public static Vector4 x1xx(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.x, a.x);
        }

        public static Vector4 y1xx(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.x, a.x);
        }

        public static Vector4 z1xx(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.x, a.x);
        }

        public static Vector4 w1xx(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.x, a.x);
        }

        public static Vector4 _10xx(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, a.x);
        }

        public static Vector4 _00xx(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, a.x);
        }

        public static Vector4 x0xx(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.x, a.x);
        }

        public static Vector4 y0xx(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.x, a.x);
        }

        public static Vector4 z0xx(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.x, a.x);
        }

        public static Vector4 w0xx(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.x, a.x);
        }

        public static Vector4 _1xxx(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.x, a.x);
        }

        public static Vector4 _0xxx(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.x, a.x);
        }

        public static Vector4 xxxx(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.x, a.x);
        }

        public static Vector4 yxxx(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.x, a.x);
        }

        public static Vector4 zxxx(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.x, a.x);
        }

        public static Vector4 wxxx(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.x, a.x);
        }

        public static Vector4 _1yxx(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.x, a.x);
        }

        public static Vector4 _0yxx(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.x, a.x);
        }

        public static Vector4 xyxx(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.x, a.x);
        }

        public static Vector4 yyxx(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.x, a.x);
        }

        public static Vector4 zyxx(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.x, a.x);
        }

        public static Vector4 wyxx(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.x, a.x);
        }

        public static Vector4 _1zxx(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.x, a.x);
        }

        public static Vector4 _0zxx(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.x, a.x);
        }

        public static Vector4 xzxx(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.x, a.x);
        }

        public static Vector4 yzxx(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.x, a.x);
        }

        public static Vector4 zzxx(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.x, a.x);
        }

        public static Vector4 wzxx(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.x, a.x);
        }

        public static Vector4 _1wxx(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.x, a.x);
        }

        public static Vector4 _0wxx(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.x, a.x);
        }

        public static Vector4 xwxx(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.x, a.x);
        }

        public static Vector4 ywxx(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.x, a.x);
        }

        public static Vector4 zwxx(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.x, a.x);
        }

        public static Vector4 wwxx(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.x, a.x);
        }

        public static Vector4 _11yx(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, a.x);
        }

        public static Vector4 _01yx(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, a.x);
        }

        public static Vector4 x1yx(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.y, a.x);
        }

        public static Vector4 y1yx(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.y, a.x);
        }

        public static Vector4 z1yx(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.y, a.x);
        }

        public static Vector4 w1yx(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.y, a.x);
        }

        public static Vector4 _10yx(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, a.x);
        }

        public static Vector4 _00yx(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, a.x);
        }

        public static Vector4 x0yx(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.y, a.x);
        }

        public static Vector4 y0yx(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.y, a.x);
        }

        public static Vector4 z0yx(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.y, a.x);
        }

        public static Vector4 w0yx(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.y, a.x);
        }

        public static Vector4 _1xyx(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.y, a.x);
        }

        public static Vector4 _0xyx(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.y, a.x);
        }

        public static Vector4 xxyx(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.y, a.x);
        }

        public static Vector4 yxyx(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.y, a.x);
        }

        public static Vector4 zxyx(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.y, a.x);
        }

        public static Vector4 wxyx(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.y, a.x);
        }

        public static Vector4 _1yyx(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.y, a.x);
        }

        public static Vector4 _0yyx(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.y, a.x);
        }

        public static Vector4 xyyx(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.y, a.x);
        }

        public static Vector4 yyyx(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.y, a.x);
        }

        public static Vector4 zyyx(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.y, a.x);
        }

        public static Vector4 wyyx(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.y, a.x);
        }

        public static Vector4 _1zyx(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.y, a.x);
        }

        public static Vector4 _0zyx(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.y, a.x);
        }

        public static Vector4 xzyx(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.y, a.x);
        }

        public static Vector4 yzyx(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.y, a.x);
        }

        public static Vector4 zzyx(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.y, a.x);
        }

        public static Vector4 wzyx(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.y, a.x);
        }

        public static Vector4 _1wyx(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.y, a.x);
        }

        public static Vector4 _0wyx(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.y, a.x);
        }

        public static Vector4 xwyx(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.y, a.x);
        }

        public static Vector4 ywyx(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.y, a.x);
        }

        public static Vector4 zwyx(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.y, a.x);
        }

        public static Vector4 wwyx(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.y, a.x);
        }

        public static Vector4 _11zx(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, a.x);
        }

        public static Vector4 _01zx(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, a.x);
        }

        public static Vector4 x1zx(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.z, a.x);
        }

        public static Vector4 y1zx(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.z, a.x);
        }

        public static Vector4 z1zx(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.z, a.x);
        }

        public static Vector4 w1zx(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.z, a.x);
        }

        public static Vector4 _10zx(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, a.x);
        }

        public static Vector4 _00zx(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, a.x);
        }

        public static Vector4 x0zx(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.z, a.x);
        }

        public static Vector4 y0zx(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.z, a.x);
        }

        public static Vector4 z0zx(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.z, a.x);
        }

        public static Vector4 w0zx(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.z, a.x);
        }

        public static Vector4 _1xzx(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.z, a.x);
        }

        public static Vector4 _0xzx(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.z, a.x);
        }

        public static Vector4 xxzx(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.z, a.x);
        }

        public static Vector4 yxzx(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.z, a.x);
        }

        public static Vector4 zxzx(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.z, a.x);
        }

        public static Vector4 wxzx(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.z, a.x);
        }

        public static Vector4 _1yzx(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.z, a.x);
        }

        public static Vector4 _0yzx(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.z, a.x);
        }

        public static Vector4 xyzx(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.z, a.x);
        }

        public static Vector4 yyzx(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.z, a.x);
        }

        public static Vector4 zyzx(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.z, a.x);
        }

        public static Vector4 wyzx(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.z, a.x);
        }

        public static Vector4 _1zzx(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.z, a.x);
        }

        public static Vector4 _0zzx(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.z, a.x);
        }

        public static Vector4 xzzx(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.z, a.x);
        }

        public static Vector4 yzzx(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.z, a.x);
        }

        public static Vector4 zzzx(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.z, a.x);
        }

        public static Vector4 wzzx(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.z, a.x);
        }

        public static Vector4 _1wzx(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.z, a.x);
        }

        public static Vector4 _0wzx(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.z, a.x);
        }

        public static Vector4 xwzx(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.z, a.x);
        }

        public static Vector4 ywzx(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.z, a.x);
        }

        public static Vector4 zwzx(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.z, a.x);
        }

        public static Vector4 wwzx(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.z, a.x);
        }

        public static Vector4 _11wx(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.w, a.x);
        }

        public static Vector4 _01wx(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.w, a.x);
        }

        public static Vector4 x1wx(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.w, a.x);
        }

        public static Vector4 y1wx(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.w, a.x);
        }

        public static Vector4 z1wx(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.w, a.x);
        }

        public static Vector4 w1wx(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.w, a.x);
        }

        public static Vector4 _10wx(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.w, a.x);
        }

        public static Vector4 _00wx(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.w, a.x);
        }

        public static Vector4 x0wx(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.w, a.x);
        }

        public static Vector4 y0wx(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.w, a.x);
        }

        public static Vector4 z0wx(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.w, a.x);
        }

        public static Vector4 w0wx(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.w, a.x);
        }

        public static Vector4 _1xwx(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.w, a.x);
        }

        public static Vector4 _0xwx(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.w, a.x);
        }

        public static Vector4 xxwx(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.w, a.x);
        }

        public static Vector4 yxwx(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.w, a.x);
        }

        public static Vector4 zxwx(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.w, a.x);
        }

        public static Vector4 wxwx(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.w, a.x);
        }

        public static Vector4 _1ywx(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.w, a.x);
        }

        public static Vector4 _0ywx(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.w, a.x);
        }

        public static Vector4 xywx(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.w, a.x);
        }

        public static Vector4 yywx(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.w, a.x);
        }

        public static Vector4 zywx(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.w, a.x);
        }

        public static Vector4 wywx(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.w, a.x);
        }

        public static Vector4 _1zwx(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.w, a.x);
        }

        public static Vector4 _0zwx(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.w, a.x);
        }

        public static Vector4 xzwx(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.w, a.x);
        }

        public static Vector4 yzwx(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.w, a.x);
        }

        public static Vector4 zzwx(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.w, a.x);
        }

        public static Vector4 wzwx(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.w, a.x);
        }

        public static Vector4 _1wwx(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.w, a.x);
        }

        public static Vector4 _0wwx(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.w, a.x);
        }

        public static Vector4 xwwx(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.w, a.x);
        }

        public static Vector4 ywwx(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.w, a.x);
        }

        public static Vector4 zwwx(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.w, a.x);
        }

        public static Vector4 wwwx(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.w, a.x);
        }

        public static Vector4 _111y(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, a.y);
        }

        public static Vector4 _011y(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, a.y);
        }

        public static Vector4 x11y(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, a.y);
        }

        public static Vector4 y11y(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, a.y);
        }

        public static Vector4 z11y(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, a.y);
        }

        public static Vector4 w11y(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 1.0f, a.y);
        }

        public static Vector4 _101y(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, a.y);
        }

        public static Vector4 _001y(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, a.y);
        }

        public static Vector4 x01y(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, a.y);
        }

        public static Vector4 y01y(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, a.y);
        }

        public static Vector4 z01y(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, a.y);
        }

        public static Vector4 w01y(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 1.0f, a.y);
        }

        public static Vector4 _1x1y(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, a.y);
        }

        public static Vector4 _0x1y(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, a.y);
        }

        public static Vector4 xx1y(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 1.0f, a.y);
        }

        public static Vector4 yx1y(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 1.0f, a.y);
        }

        public static Vector4 zx1y(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 1.0f, a.y);
        }

        public static Vector4 wx1y(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 1.0f, a.y);
        }

        public static Vector4 _1y1y(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, a.y);
        }

        public static Vector4 _0y1y(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, a.y);
        }

        public static Vector4 xy1y(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 1.0f, a.y);
        }

        public static Vector4 yy1y(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 1.0f, a.y);
        }

        public static Vector4 zy1y(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 1.0f, a.y);
        }

        public static Vector4 wy1y(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 1.0f, a.y);
        }

        public static Vector4 _1z1y(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, a.y);
        }

        public static Vector4 _0z1y(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, a.y);
        }

        public static Vector4 xz1y(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 1.0f, a.y);
        }

        public static Vector4 yz1y(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 1.0f, a.y);
        }

        public static Vector4 zz1y(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 1.0f, a.y);
        }

        public static Vector4 wz1y(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 1.0f, a.y);
        }

        public static Vector4 _1w1y(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 1.0f, a.y);
        }

        public static Vector4 _0w1y(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 1.0f, a.y);
        }

        public static Vector4 xw1y(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 1.0f, a.y);
        }

        public static Vector4 yw1y(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 1.0f, a.y);
        }

        public static Vector4 zw1y(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 1.0f, a.y);
        }

        public static Vector4 ww1y(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 1.0f, a.y);
        }

        public static Vector4 _110y(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, a.y);
        }

        public static Vector4 _010y(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, a.y);
        }

        public static Vector4 x10y(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, a.y);
        }

        public static Vector4 y10y(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, a.y);
        }

        public static Vector4 z10y(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, a.y);
        }

        public static Vector4 w10y(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 0.0f, a.y);
        }

        public static Vector4 _100y(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, a.y);
        }

        public static Vector4 _000y(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, a.y);
        }

        public static Vector4 x00y(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, a.y);
        }

        public static Vector4 y00y(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, a.y);
        }

        public static Vector4 z00y(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, a.y);
        }

        public static Vector4 w00y(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 0.0f, a.y);
        }

        public static Vector4 _1x0y(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, a.y);
        }

        public static Vector4 _0x0y(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, a.y);
        }

        public static Vector4 xx0y(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 0.0f, a.y);
        }

        public static Vector4 yx0y(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 0.0f, a.y);
        }

        public static Vector4 zx0y(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 0.0f, a.y);
        }

        public static Vector4 wx0y(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 0.0f, a.y);
        }

        public static Vector4 _1y0y(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, a.y);
        }

        public static Vector4 _0y0y(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, a.y);
        }

        public static Vector4 xy0y(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 0.0f, a.y);
        }

        public static Vector4 yy0y(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 0.0f, a.y);
        }

        public static Vector4 zy0y(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 0.0f, a.y);
        }

        public static Vector4 wy0y(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 0.0f, a.y);
        }

        public static Vector4 _1z0y(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, a.y);
        }

        public static Vector4 _0z0y(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, a.y);
        }

        public static Vector4 xz0y(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 0.0f, a.y);
        }

        public static Vector4 yz0y(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 0.0f, a.y);
        }

        public static Vector4 zz0y(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 0.0f, a.y);
        }

        public static Vector4 wz0y(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 0.0f, a.y);
        }

        public static Vector4 _1w0y(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 0.0f, a.y);
        }

        public static Vector4 _0w0y(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 0.0f, a.y);
        }

        public static Vector4 xw0y(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 0.0f, a.y);
        }

        public static Vector4 yw0y(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 0.0f, a.y);
        }

        public static Vector4 zw0y(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 0.0f, a.y);
        }

        public static Vector4 ww0y(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 0.0f, a.y);
        }

        public static Vector4 _11xy(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, a.y);
        }

        public static Vector4 _01xy(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, a.y);
        }

        public static Vector4 x1xy(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.x, a.y);
        }

        public static Vector4 y1xy(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.x, a.y);
        }

        public static Vector4 z1xy(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.x, a.y);
        }

        public static Vector4 w1xy(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.x, a.y);
        }

        public static Vector4 _10xy(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, a.y);
        }

        public static Vector4 _00xy(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, a.y);
        }

        public static Vector4 x0xy(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.x, a.y);
        }

        public static Vector4 y0xy(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.x, a.y);
        }

        public static Vector4 z0xy(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.x, a.y);
        }

        public static Vector4 w0xy(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.x, a.y);
        }

        public static Vector4 _1xxy(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.x, a.y);
        }

        public static Vector4 _0xxy(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.x, a.y);
        }

        public static Vector4 xxxy(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.x, a.y);
        }

        public static Vector4 yxxy(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.x, a.y);
        }

        public static Vector4 zxxy(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.x, a.y);
        }

        public static Vector4 wxxy(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.x, a.y);
        }

        public static Vector4 _1yxy(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.x, a.y);
        }

        public static Vector4 _0yxy(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.x, a.y);
        }

        public static Vector4 xyxy(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.x, a.y);
        }

        public static Vector4 yyxy(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.x, a.y);
        }

        public static Vector4 zyxy(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.x, a.y);
        }

        public static Vector4 wyxy(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.x, a.y);
        }

        public static Vector4 _1zxy(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.x, a.y);
        }

        public static Vector4 _0zxy(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.x, a.y);
        }

        public static Vector4 xzxy(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.x, a.y);
        }

        public static Vector4 yzxy(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.x, a.y);
        }

        public static Vector4 zzxy(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.x, a.y);
        }

        public static Vector4 wzxy(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.x, a.y);
        }

        public static Vector4 _1wxy(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.x, a.y);
        }

        public static Vector4 _0wxy(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.x, a.y);
        }

        public static Vector4 xwxy(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.x, a.y);
        }

        public static Vector4 ywxy(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.x, a.y);
        }

        public static Vector4 zwxy(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.x, a.y);
        }

        public static Vector4 wwxy(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.x, a.y);
        }

        public static Vector4 _11yy(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, a.y);
        }

        public static Vector4 _01yy(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, a.y);
        }

        public static Vector4 x1yy(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.y, a.y);
        }

        public static Vector4 y1yy(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.y, a.y);
        }

        public static Vector4 z1yy(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.y, a.y);
        }

        public static Vector4 w1yy(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.y, a.y);
        }

        public static Vector4 _10yy(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, a.y);
        }

        public static Vector4 _00yy(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, a.y);
        }

        public static Vector4 x0yy(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.y, a.y);
        }

        public static Vector4 y0yy(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.y, a.y);
        }

        public static Vector4 z0yy(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.y, a.y);
        }

        public static Vector4 w0yy(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.y, a.y);
        }

        public static Vector4 _1xyy(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.y, a.y);
        }

        public static Vector4 _0xyy(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.y, a.y);
        }

        public static Vector4 xxyy(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.y, a.y);
        }

        public static Vector4 yxyy(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.y, a.y);
        }

        public static Vector4 zxyy(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.y, a.y);
        }

        public static Vector4 wxyy(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.y, a.y);
        }

        public static Vector4 _1yyy(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.y, a.y);
        }

        public static Vector4 _0yyy(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.y, a.y);
        }

        public static Vector4 xyyy(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.y, a.y);
        }

        public static Vector4 yyyy(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.y, a.y);
        }

        public static Vector4 zyyy(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.y, a.y);
        }

        public static Vector4 wyyy(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.y, a.y);
        }

        public static Vector4 _1zyy(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.y, a.y);
        }

        public static Vector4 _0zyy(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.y, a.y);
        }

        public static Vector4 xzyy(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.y, a.y);
        }

        public static Vector4 yzyy(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.y, a.y);
        }

        public static Vector4 zzyy(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.y, a.y);
        }

        public static Vector4 wzyy(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.y, a.y);
        }

        public static Vector4 _1wyy(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.y, a.y);
        }

        public static Vector4 _0wyy(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.y, a.y);
        }

        public static Vector4 xwyy(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.y, a.y);
        }

        public static Vector4 ywyy(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.y, a.y);
        }

        public static Vector4 zwyy(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.y, a.y);
        }

        public static Vector4 wwyy(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.y, a.y);
        }

        public static Vector4 _11zy(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, a.y);
        }

        public static Vector4 _01zy(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, a.y);
        }

        public static Vector4 x1zy(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.z, a.y);
        }

        public static Vector4 y1zy(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.z, a.y);
        }

        public static Vector4 z1zy(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.z, a.y);
        }

        public static Vector4 w1zy(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.z, a.y);
        }

        public static Vector4 _10zy(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, a.y);
        }

        public static Vector4 _00zy(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, a.y);
        }

        public static Vector4 x0zy(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.z, a.y);
        }

        public static Vector4 y0zy(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.z, a.y);
        }

        public static Vector4 z0zy(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.z, a.y);
        }

        public static Vector4 w0zy(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.z, a.y);
        }

        public static Vector4 _1xzy(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.z, a.y);
        }

        public static Vector4 _0xzy(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.z, a.y);
        }

        public static Vector4 xxzy(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.z, a.y);
        }

        public static Vector4 yxzy(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.z, a.y);
        }

        public static Vector4 zxzy(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.z, a.y);
        }

        public static Vector4 wxzy(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.z, a.y);
        }

        public static Vector4 _1yzy(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.z, a.y);
        }

        public static Vector4 _0yzy(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.z, a.y);
        }

        public static Vector4 xyzy(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.z, a.y);
        }

        public static Vector4 yyzy(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.z, a.y);
        }

        public static Vector4 zyzy(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.z, a.y);
        }

        public static Vector4 wyzy(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.z, a.y);
        }

        public static Vector4 _1zzy(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.z, a.y);
        }

        public static Vector4 _0zzy(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.z, a.y);
        }

        public static Vector4 xzzy(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.z, a.y);
        }

        public static Vector4 yzzy(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.z, a.y);
        }

        public static Vector4 zzzy(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.z, a.y);
        }

        public static Vector4 wzzy(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.z, a.y);
        }

        public static Vector4 _1wzy(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.z, a.y);
        }

        public static Vector4 _0wzy(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.z, a.y);
        }

        public static Vector4 xwzy(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.z, a.y);
        }

        public static Vector4 ywzy(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.z, a.y);
        }

        public static Vector4 zwzy(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.z, a.y);
        }

        public static Vector4 wwzy(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.z, a.y);
        }

        public static Vector4 _11wy(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.w, a.y);
        }

        public static Vector4 _01wy(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.w, a.y);
        }

        public static Vector4 x1wy(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.w, a.y);
        }

        public static Vector4 y1wy(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.w, a.y);
        }

        public static Vector4 z1wy(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.w, a.y);
        }

        public static Vector4 w1wy(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.w, a.y);
        }

        public static Vector4 _10wy(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.w, a.y);
        }

        public static Vector4 _00wy(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.w, a.y);
        }

        public static Vector4 x0wy(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.w, a.y);
        }

        public static Vector4 y0wy(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.w, a.y);
        }

        public static Vector4 z0wy(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.w, a.y);
        }

        public static Vector4 w0wy(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.w, a.y);
        }

        public static Vector4 _1xwy(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.w, a.y);
        }

        public static Vector4 _0xwy(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.w, a.y);
        }

        public static Vector4 xxwy(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.w, a.y);
        }

        public static Vector4 yxwy(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.w, a.y);
        }

        public static Vector4 zxwy(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.w, a.y);
        }

        public static Vector4 wxwy(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.w, a.y);
        }

        public static Vector4 _1ywy(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.w, a.y);
        }

        public static Vector4 _0ywy(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.w, a.y);
        }

        public static Vector4 xywy(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.w, a.y);
        }

        public static Vector4 yywy(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.w, a.y);
        }

        public static Vector4 zywy(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.w, a.y);
        }

        public static Vector4 wywy(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.w, a.y);
        }

        public static Vector4 _1zwy(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.w, a.y);
        }

        public static Vector4 _0zwy(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.w, a.y);
        }

        public static Vector4 xzwy(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.w, a.y);
        }

        public static Vector4 yzwy(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.w, a.y);
        }

        public static Vector4 zzwy(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.w, a.y);
        }

        public static Vector4 wzwy(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.w, a.y);
        }

        public static Vector4 _1wwy(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.w, a.y);
        }

        public static Vector4 _0wwy(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.w, a.y);
        }

        public static Vector4 xwwy(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.w, a.y);
        }

        public static Vector4 ywwy(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.w, a.y);
        }

        public static Vector4 zwwy(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.w, a.y);
        }

        public static Vector4 wwwy(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.w, a.y);
        }

        public static Vector4 _111z(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, a.z);
        }

        public static Vector4 _011z(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, a.z);
        }

        public static Vector4 x11z(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, a.z);
        }

        public static Vector4 y11z(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, a.z);
        }

        public static Vector4 z11z(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, a.z);
        }

        public static Vector4 w11z(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 1.0f, a.z);
        }

        public static Vector4 _101z(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, a.z);
        }

        public static Vector4 _001z(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, a.z);
        }

        public static Vector4 x01z(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, a.z);
        }

        public static Vector4 y01z(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, a.z);
        }

        public static Vector4 z01z(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, a.z);
        }

        public static Vector4 w01z(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 1.0f, a.z);
        }

        public static Vector4 _1x1z(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, a.z);
        }

        public static Vector4 _0x1z(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, a.z);
        }

        public static Vector4 xx1z(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 1.0f, a.z);
        }

        public static Vector4 yx1z(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 1.0f, a.z);
        }

        public static Vector4 zx1z(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 1.0f, a.z);
        }

        public static Vector4 wx1z(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 1.0f, a.z);
        }

        public static Vector4 _1y1z(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, a.z);
        }

        public static Vector4 _0y1z(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, a.z);
        }

        public static Vector4 xy1z(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 1.0f, a.z);
        }

        public static Vector4 yy1z(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 1.0f, a.z);
        }

        public static Vector4 zy1z(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 1.0f, a.z);
        }

        public static Vector4 wy1z(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 1.0f, a.z);
        }

        public static Vector4 _1z1z(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, a.z);
        }

        public static Vector4 _0z1z(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, a.z);
        }

        public static Vector4 xz1z(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 1.0f, a.z);
        }

        public static Vector4 yz1z(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 1.0f, a.z);
        }

        public static Vector4 zz1z(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 1.0f, a.z);
        }

        public static Vector4 wz1z(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 1.0f, a.z);
        }

        public static Vector4 _1w1z(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 1.0f, a.z);
        }

        public static Vector4 _0w1z(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 1.0f, a.z);
        }

        public static Vector4 xw1z(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 1.0f, a.z);
        }

        public static Vector4 yw1z(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 1.0f, a.z);
        }

        public static Vector4 zw1z(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 1.0f, a.z);
        }

        public static Vector4 ww1z(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 1.0f, a.z);
        }

        public static Vector4 _110z(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, a.z);
        }

        public static Vector4 _010z(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, a.z);
        }

        public static Vector4 x10z(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, a.z);
        }

        public static Vector4 y10z(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, a.z);
        }

        public static Vector4 z10z(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, a.z);
        }

        public static Vector4 w10z(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 0.0f, a.z);
        }

        public static Vector4 _100z(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, a.z);
        }

        public static Vector4 _000z(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, a.z);
        }

        public static Vector4 x00z(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, a.z);
        }

        public static Vector4 y00z(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, a.z);
        }

        public static Vector4 z00z(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, a.z);
        }

        public static Vector4 w00z(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 0.0f, a.z);
        }

        public static Vector4 _1x0z(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, a.z);
        }

        public static Vector4 _0x0z(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, a.z);
        }

        public static Vector4 xx0z(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 0.0f, a.z);
        }

        public static Vector4 yx0z(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 0.0f, a.z);
        }

        public static Vector4 zx0z(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 0.0f, a.z);
        }

        public static Vector4 wx0z(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 0.0f, a.z);
        }

        public static Vector4 _1y0z(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, a.z);
        }

        public static Vector4 _0y0z(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, a.z);
        }

        public static Vector4 xy0z(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 0.0f, a.z);
        }

        public static Vector4 yy0z(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 0.0f, a.z);
        }

        public static Vector4 zy0z(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 0.0f, a.z);
        }

        public static Vector4 wy0z(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 0.0f, a.z);
        }

        public static Vector4 _1z0z(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, a.z);
        }

        public static Vector4 _0z0z(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, a.z);
        }

        public static Vector4 xz0z(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 0.0f, a.z);
        }

        public static Vector4 yz0z(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 0.0f, a.z);
        }

        public static Vector4 zz0z(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 0.0f, a.z);
        }

        public static Vector4 wz0z(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 0.0f, a.z);
        }

        public static Vector4 _1w0z(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 0.0f, a.z);
        }

        public static Vector4 _0w0z(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 0.0f, a.z);
        }

        public static Vector4 xw0z(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 0.0f, a.z);
        }

        public static Vector4 yw0z(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 0.0f, a.z);
        }

        public static Vector4 zw0z(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 0.0f, a.z);
        }

        public static Vector4 ww0z(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 0.0f, a.z);
        }

        public static Vector4 _11xz(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, a.z);
        }

        public static Vector4 _01xz(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, a.z);
        }

        public static Vector4 x1xz(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.x, a.z);
        }

        public static Vector4 y1xz(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.x, a.z);
        }

        public static Vector4 z1xz(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.x, a.z);
        }

        public static Vector4 w1xz(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.x, a.z);
        }

        public static Vector4 _10xz(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, a.z);
        }

        public static Vector4 _00xz(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, a.z);
        }

        public static Vector4 x0xz(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.x, a.z);
        }

        public static Vector4 y0xz(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.x, a.z);
        }

        public static Vector4 z0xz(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.x, a.z);
        }

        public static Vector4 w0xz(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.x, a.z);
        }

        public static Vector4 _1xxz(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.x, a.z);
        }

        public static Vector4 _0xxz(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.x, a.z);
        }

        public static Vector4 xxxz(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.x, a.z);
        }

        public static Vector4 yxxz(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.x, a.z);
        }

        public static Vector4 zxxz(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.x, a.z);
        }

        public static Vector4 wxxz(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.x, a.z);
        }

        public static Vector4 _1yxz(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.x, a.z);
        }

        public static Vector4 _0yxz(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.x, a.z);
        }

        public static Vector4 xyxz(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.x, a.z);
        }

        public static Vector4 yyxz(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.x, a.z);
        }

        public static Vector4 zyxz(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.x, a.z);
        }

        public static Vector4 wyxz(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.x, a.z);
        }

        public static Vector4 _1zxz(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.x, a.z);
        }

        public static Vector4 _0zxz(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.x, a.z);
        }

        public static Vector4 xzxz(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.x, a.z);
        }

        public static Vector4 yzxz(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.x, a.z);
        }

        public static Vector4 zzxz(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.x, a.z);
        }

        public static Vector4 wzxz(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.x, a.z);
        }

        public static Vector4 _1wxz(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.x, a.z);
        }

        public static Vector4 _0wxz(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.x, a.z);
        }

        public static Vector4 xwxz(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.x, a.z);
        }

        public static Vector4 ywxz(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.x, a.z);
        }

        public static Vector4 zwxz(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.x, a.z);
        }

        public static Vector4 wwxz(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.x, a.z);
        }

        public static Vector4 _11yz(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, a.z);
        }

        public static Vector4 _01yz(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, a.z);
        }

        public static Vector4 x1yz(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.y, a.z);
        }

        public static Vector4 y1yz(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.y, a.z);
        }

        public static Vector4 z1yz(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.y, a.z);
        }

        public static Vector4 w1yz(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.y, a.z);
        }

        public static Vector4 _10yz(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, a.z);
        }

        public static Vector4 _00yz(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, a.z);
        }

        public static Vector4 x0yz(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.y, a.z);
        }

        public static Vector4 y0yz(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.y, a.z);
        }

        public static Vector4 z0yz(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.y, a.z);
        }

        public static Vector4 w0yz(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.y, a.z);
        }

        public static Vector4 _1xyz(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.y, a.z);
        }

        public static Vector4 _0xyz(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.y, a.z);
        }

        public static Vector4 xxyz(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.y, a.z);
        }

        public static Vector4 yxyz(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.y, a.z);
        }

        public static Vector4 zxyz(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.y, a.z);
        }

        public static Vector4 wxyz(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.y, a.z);
        }

        public static Vector4 _1yyz(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.y, a.z);
        }

        public static Vector4 _0yyz(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.y, a.z);
        }

        public static Vector4 xyyz(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.y, a.z);
        }

        public static Vector4 yyyz(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.y, a.z);
        }

        public static Vector4 zyyz(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.y, a.z);
        }

        public static Vector4 wyyz(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.y, a.z);
        }

        public static Vector4 _1zyz(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.y, a.z);
        }

        public static Vector4 _0zyz(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.y, a.z);
        }

        public static Vector4 xzyz(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.y, a.z);
        }

        public static Vector4 yzyz(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.y, a.z);
        }

        public static Vector4 zzyz(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.y, a.z);
        }

        public static Vector4 wzyz(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.y, a.z);
        }

        public static Vector4 _1wyz(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.y, a.z);
        }

        public static Vector4 _0wyz(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.y, a.z);
        }

        public static Vector4 xwyz(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.y, a.z);
        }

        public static Vector4 ywyz(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.y, a.z);
        }

        public static Vector4 zwyz(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.y, a.z);
        }

        public static Vector4 wwyz(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.y, a.z);
        }

        public static Vector4 _11zz(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, a.z);
        }

        public static Vector4 _01zz(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, a.z);
        }

        public static Vector4 x1zz(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.z, a.z);
        }

        public static Vector4 y1zz(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.z, a.z);
        }

        public static Vector4 z1zz(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.z, a.z);
        }

        public static Vector4 w1zz(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.z, a.z);
        }

        public static Vector4 _10zz(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, a.z);
        }

        public static Vector4 _00zz(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, a.z);
        }

        public static Vector4 x0zz(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.z, a.z);
        }

        public static Vector4 y0zz(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.z, a.z);
        }

        public static Vector4 z0zz(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.z, a.z);
        }

        public static Vector4 w0zz(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.z, a.z);
        }

        public static Vector4 _1xzz(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.z, a.z);
        }

        public static Vector4 _0xzz(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.z, a.z);
        }

        public static Vector4 xxzz(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.z, a.z);
        }

        public static Vector4 yxzz(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.z, a.z);
        }

        public static Vector4 zxzz(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.z, a.z);
        }

        public static Vector4 wxzz(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.z, a.z);
        }

        public static Vector4 _1yzz(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.z, a.z);
        }

        public static Vector4 _0yzz(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.z, a.z);
        }

        public static Vector4 xyzz(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.z, a.z);
        }

        public static Vector4 yyzz(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.z, a.z);
        }

        public static Vector4 zyzz(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.z, a.z);
        }

        public static Vector4 wyzz(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.z, a.z);
        }

        public static Vector4 _1zzz(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.z, a.z);
        }

        public static Vector4 _0zzz(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.z, a.z);
        }

        public static Vector4 xzzz(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.z, a.z);
        }

        public static Vector4 yzzz(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.z, a.z);
        }

        public static Vector4 zzzz(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.z, a.z);
        }

        public static Vector4 wzzz(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.z, a.z);
        }

        public static Vector4 _1wzz(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.z, a.z);
        }

        public static Vector4 _0wzz(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.z, a.z);
        }

        public static Vector4 xwzz(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.z, a.z);
        }

        public static Vector4 ywzz(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.z, a.z);
        }

        public static Vector4 zwzz(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.z, a.z);
        }

        public static Vector4 wwzz(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.z, a.z);
        }

        public static Vector4 _11wz(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.w, a.z);
        }

        public static Vector4 _01wz(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.w, a.z);
        }

        public static Vector4 x1wz(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.w, a.z);
        }

        public static Vector4 y1wz(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.w, a.z);
        }

        public static Vector4 z1wz(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.w, a.z);
        }

        public static Vector4 w1wz(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.w, a.z);
        }

        public static Vector4 _10wz(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.w, a.z);
        }

        public static Vector4 _00wz(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.w, a.z);
        }

        public static Vector4 x0wz(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.w, a.z);
        }

        public static Vector4 y0wz(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.w, a.z);
        }

        public static Vector4 z0wz(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.w, a.z);
        }

        public static Vector4 w0wz(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.w, a.z);
        }

        public static Vector4 _1xwz(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.w, a.z);
        }

        public static Vector4 _0xwz(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.w, a.z);
        }

        public static Vector4 xxwz(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.w, a.z);
        }

        public static Vector4 yxwz(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.w, a.z);
        }

        public static Vector4 zxwz(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.w, a.z);
        }

        public static Vector4 wxwz(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.w, a.z);
        }

        public static Vector4 _1ywz(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.w, a.z);
        }

        public static Vector4 _0ywz(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.w, a.z);
        }

        public static Vector4 xywz(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.w, a.z);
        }

        public static Vector4 yywz(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.w, a.z);
        }

        public static Vector4 zywz(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.w, a.z);
        }

        public static Vector4 wywz(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.w, a.z);
        }

        public static Vector4 _1zwz(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.w, a.z);
        }

        public static Vector4 _0zwz(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.w, a.z);
        }

        public static Vector4 xzwz(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.w, a.z);
        }

        public static Vector4 yzwz(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.w, a.z);
        }

        public static Vector4 zzwz(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.w, a.z);
        }

        public static Vector4 wzwz(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.w, a.z);
        }

        public static Vector4 _1wwz(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.w, a.z);
        }

        public static Vector4 _0wwz(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.w, a.z);
        }

        public static Vector4 xwwz(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.w, a.z);
        }

        public static Vector4 ywwz(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.w, a.z);
        }

        public static Vector4 zwwz(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.w, a.z);
        }

        public static Vector4 wwwz(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.w, a.z);
        }

        public static Vector4 _111w(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 1.0f, a.w);
        }

        public static Vector4 _011w(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 1.0f, a.w);
        }

        public static Vector4 x11w(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 1.0f, a.w);
        }

        public static Vector4 y11w(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 1.0f, a.w);
        }

        public static Vector4 z11w(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 1.0f, a.w);
        }

        public static Vector4 w11w(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 1.0f, a.w);
        }

        public static Vector4 _101w(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 1.0f, a.w);
        }

        public static Vector4 _001w(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 1.0f, a.w);
        }

        public static Vector4 x01w(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 1.0f, a.w);
        }

        public static Vector4 y01w(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 1.0f, a.w);
        }

        public static Vector4 z01w(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 1.0f, a.w);
        }

        public static Vector4 w01w(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 1.0f, a.w);
        }

        public static Vector4 _1x1w(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 1.0f, a.w);
        }

        public static Vector4 _0x1w(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 1.0f, a.w);
        }

        public static Vector4 xx1w(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 1.0f, a.w);
        }

        public static Vector4 yx1w(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 1.0f, a.w);
        }

        public static Vector4 zx1w(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 1.0f, a.w);
        }

        public static Vector4 wx1w(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 1.0f, a.w);
        }

        public static Vector4 _1y1w(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 1.0f, a.w);
        }

        public static Vector4 _0y1w(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 1.0f, a.w);
        }

        public static Vector4 xy1w(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 1.0f, a.w);
        }

        public static Vector4 yy1w(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 1.0f, a.w);
        }

        public static Vector4 zy1w(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 1.0f, a.w);
        }

        public static Vector4 wy1w(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 1.0f, a.w);
        }

        public static Vector4 _1z1w(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 1.0f, a.w);
        }

        public static Vector4 _0z1w(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 1.0f, a.w);
        }

        public static Vector4 xz1w(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 1.0f, a.w);
        }

        public static Vector4 yz1w(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 1.0f, a.w);
        }

        public static Vector4 zz1w(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 1.0f, a.w);
        }

        public static Vector4 wz1w(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 1.0f, a.w);
        }

        public static Vector4 _1w1w(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 1.0f, a.w);
        }

        public static Vector4 _0w1w(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 1.0f, a.w);
        }

        public static Vector4 xw1w(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 1.0f, a.w);
        }

        public static Vector4 yw1w(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 1.0f, a.w);
        }

        public static Vector4 zw1w(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 1.0f, a.w);
        }

        public static Vector4 ww1w(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 1.0f, a.w);
        }

        public static Vector4 _110w(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, 0.0f, a.w);
        }

        public static Vector4 _010w(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, 0.0f, a.w);
        }

        public static Vector4 x10w(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, 0.0f, a.w);
        }

        public static Vector4 y10w(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, 0.0f, a.w);
        }

        public static Vector4 z10w(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, 0.0f, a.w);
        }

        public static Vector4 w10w(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, 0.0f, a.w);
        }

        public static Vector4 _100w(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, 0.0f, a.w);
        }

        public static Vector4 _000w(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, 0.0f, a.w);
        }

        public static Vector4 x00w(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, 0.0f, a.w);
        }

        public static Vector4 y00w(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, 0.0f, a.w);
        }

        public static Vector4 z00w(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, 0.0f, a.w);
        }

        public static Vector4 w00w(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, 0.0f, a.w);
        }

        public static Vector4 _1x0w(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, 0.0f, a.w);
        }

        public static Vector4 _0x0w(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, 0.0f, a.w);
        }

        public static Vector4 xx0w(this Vector4 a)
        {
            return new Vector4(a.x, a.x, 0.0f, a.w);
        }

        public static Vector4 yx0w(this Vector4 a)
        {
            return new Vector4(a.y, a.x, 0.0f, a.w);
        }

        public static Vector4 zx0w(this Vector4 a)
        {
            return new Vector4(a.z, a.x, 0.0f, a.w);
        }

        public static Vector4 wx0w(this Vector4 a)
        {
            return new Vector4(a.w, a.x, 0.0f, a.w);
        }

        public static Vector4 _1y0w(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, 0.0f, a.w);
        }

        public static Vector4 _0y0w(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, 0.0f, a.w);
        }

        public static Vector4 xy0w(this Vector4 a)
        {
            return new Vector4(a.x, a.y, 0.0f, a.w);
        }

        public static Vector4 yy0w(this Vector4 a)
        {
            return new Vector4(a.y, a.y, 0.0f, a.w);
        }

        public static Vector4 zy0w(this Vector4 a)
        {
            return new Vector4(a.z, a.y, 0.0f, a.w);
        }

        public static Vector4 wy0w(this Vector4 a)
        {
            return new Vector4(a.w, a.y, 0.0f, a.w);
        }

        public static Vector4 _1z0w(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, 0.0f, a.w);
        }

        public static Vector4 _0z0w(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, 0.0f, a.w);
        }

        public static Vector4 xz0w(this Vector4 a)
        {
            return new Vector4(a.x, a.z, 0.0f, a.w);
        }

        public static Vector4 yz0w(this Vector4 a)
        {
            return new Vector4(a.y, a.z, 0.0f, a.w);
        }

        public static Vector4 zz0w(this Vector4 a)
        {
            return new Vector4(a.z, a.z, 0.0f, a.w);
        }

        public static Vector4 wz0w(this Vector4 a)
        {
            return new Vector4(a.w, a.z, 0.0f, a.w);
        }

        public static Vector4 _1w0w(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, 0.0f, a.w);
        }

        public static Vector4 _0w0w(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, 0.0f, a.w);
        }

        public static Vector4 xw0w(this Vector4 a)
        {
            return new Vector4(a.x, a.w, 0.0f, a.w);
        }

        public static Vector4 yw0w(this Vector4 a)
        {
            return new Vector4(a.y, a.w, 0.0f, a.w);
        }

        public static Vector4 zw0w(this Vector4 a)
        {
            return new Vector4(a.z, a.w, 0.0f, a.w);
        }

        public static Vector4 ww0w(this Vector4 a)
        {
            return new Vector4(a.w, a.w, 0.0f, a.w);
        }

        public static Vector4 _11xw(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.x, a.w);
        }

        public static Vector4 _01xw(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.x, a.w);
        }

        public static Vector4 x1xw(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.x, a.w);
        }

        public static Vector4 y1xw(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.x, a.w);
        }

        public static Vector4 z1xw(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.x, a.w);
        }

        public static Vector4 w1xw(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.x, a.w);
        }

        public static Vector4 _10xw(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.x, a.w);
        }

        public static Vector4 _00xw(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.x, a.w);
        }

        public static Vector4 x0xw(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.x, a.w);
        }

        public static Vector4 y0xw(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.x, a.w);
        }

        public static Vector4 z0xw(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.x, a.w);
        }

        public static Vector4 w0xw(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.x, a.w);
        }

        public static Vector4 _1xxw(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.x, a.w);
        }

        public static Vector4 _0xxw(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.x, a.w);
        }

        public static Vector4 xxxw(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.x, a.w);
        }

        public static Vector4 yxxw(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.x, a.w);
        }

        public static Vector4 zxxw(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.x, a.w);
        }

        public static Vector4 wxxw(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.x, a.w);
        }

        public static Vector4 _1yxw(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.x, a.w);
        }

        public static Vector4 _0yxw(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.x, a.w);
        }

        public static Vector4 xyxw(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.x, a.w);
        }

        public static Vector4 yyxw(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.x, a.w);
        }

        public static Vector4 zyxw(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.x, a.w);
        }

        public static Vector4 wyxw(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.x, a.w);
        }

        public static Vector4 _1zxw(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.x, a.w);
        }

        public static Vector4 _0zxw(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.x, a.w);
        }

        public static Vector4 xzxw(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.x, a.w);
        }

        public static Vector4 yzxw(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.x, a.w);
        }

        public static Vector4 zzxw(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.x, a.w);
        }

        public static Vector4 wzxw(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.x, a.w);
        }

        public static Vector4 _1wxw(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.x, a.w);
        }

        public static Vector4 _0wxw(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.x, a.w);
        }

        public static Vector4 xwxw(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.x, a.w);
        }

        public static Vector4 ywxw(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.x, a.w);
        }

        public static Vector4 zwxw(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.x, a.w);
        }

        public static Vector4 wwxw(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.x, a.w);
        }

        public static Vector4 _11yw(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.y, a.w);
        }

        public static Vector4 _01yw(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.y, a.w);
        }

        public static Vector4 x1yw(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.y, a.w);
        }

        public static Vector4 y1yw(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.y, a.w);
        }

        public static Vector4 z1yw(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.y, a.w);
        }

        public static Vector4 w1yw(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.y, a.w);
        }

        public static Vector4 _10yw(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.y, a.w);
        }

        public static Vector4 _00yw(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.y, a.w);
        }

        public static Vector4 x0yw(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.y, a.w);
        }

        public static Vector4 y0yw(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.y, a.w);
        }

        public static Vector4 z0yw(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.y, a.w);
        }

        public static Vector4 w0yw(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.y, a.w);
        }

        public static Vector4 _1xyw(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.y, a.w);
        }

        public static Vector4 _0xyw(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.y, a.w);
        }

        public static Vector4 xxyw(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.y, a.w);
        }

        public static Vector4 yxyw(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.y, a.w);
        }

        public static Vector4 zxyw(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.y, a.w);
        }

        public static Vector4 wxyw(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.y, a.w);
        }

        public static Vector4 _1yyw(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.y, a.w);
        }

        public static Vector4 _0yyw(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.y, a.w);
        }

        public static Vector4 xyyw(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.y, a.w);
        }

        public static Vector4 yyyw(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.y, a.w);
        }

        public static Vector4 zyyw(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.y, a.w);
        }

        public static Vector4 wyyw(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.y, a.w);
        }

        public static Vector4 _1zyw(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.y, a.w);
        }

        public static Vector4 _0zyw(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.y, a.w);
        }

        public static Vector4 xzyw(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.y, a.w);
        }

        public static Vector4 yzyw(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.y, a.w);
        }

        public static Vector4 zzyw(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.y, a.w);
        }

        public static Vector4 wzyw(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.y, a.w);
        }

        public static Vector4 _1wyw(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.y, a.w);
        }

        public static Vector4 _0wyw(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.y, a.w);
        }

        public static Vector4 xwyw(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.y, a.w);
        }

        public static Vector4 ywyw(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.y, a.w);
        }

        public static Vector4 zwyw(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.y, a.w);
        }

        public static Vector4 wwyw(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.y, a.w);
        }

        public static Vector4 _11zw(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.z, a.w);
        }

        public static Vector4 _01zw(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.z, a.w);
        }

        public static Vector4 x1zw(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.z, a.w);
        }

        public static Vector4 y1zw(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.z, a.w);
        }

        public static Vector4 z1zw(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.z, a.w);
        }

        public static Vector4 w1zw(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.z, a.w);
        }

        public static Vector4 _10zw(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.z, a.w);
        }

        public static Vector4 _00zw(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.z, a.w);
        }

        public static Vector4 x0zw(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.z, a.w);
        }

        public static Vector4 y0zw(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.z, a.w);
        }

        public static Vector4 z0zw(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.z, a.w);
        }

        public static Vector4 w0zw(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.z, a.w);
        }

        public static Vector4 _1xzw(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.z, a.w);
        }

        public static Vector4 _0xzw(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.z, a.w);
        }

        public static Vector4 xxzw(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.z, a.w);
        }

        public static Vector4 yxzw(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.z, a.w);
        }

        public static Vector4 zxzw(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.z, a.w);
        }

        public static Vector4 wxzw(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.z, a.w);
        }

        public static Vector4 _1yzw(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.z, a.w);
        }

        public static Vector4 _0yzw(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.z, a.w);
        }

        public static Vector4 xyzw(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.z, a.w);
        }

        public static Vector4 yyzw(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.z, a.w);
        }

        public static Vector4 zyzw(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.z, a.w);
        }

        public static Vector4 wyzw(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.z, a.w);
        }

        public static Vector4 _1zzw(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.z, a.w);
        }

        public static Vector4 _0zzw(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.z, a.w);
        }

        public static Vector4 xzzw(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.z, a.w);
        }

        public static Vector4 yzzw(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.z, a.w);
        }

        public static Vector4 zzzw(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.z, a.w);
        }

        public static Vector4 wzzw(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.z, a.w);
        }

        public static Vector4 _1wzw(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.z, a.w);
        }

        public static Vector4 _0wzw(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.z, a.w);
        }

        public static Vector4 xwzw(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.z, a.w);
        }

        public static Vector4 ywzw(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.z, a.w);
        }

        public static Vector4 zwzw(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.z, a.w);
        }

        public static Vector4 wwzw(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.z, a.w);
        }

        public static Vector4 _11ww(this Vector4 a)
        {
            return new Vector4(1.0f, 1.0f, a.w, a.w);
        }

        public static Vector4 _01ww(this Vector4 a)
        {
            return new Vector4(0.0f, 1.0f, a.w, a.w);
        }

        public static Vector4 x1ww(this Vector4 a)
        {
            return new Vector4(a.x, 1.0f, a.w, a.w);
        }

        public static Vector4 y1ww(this Vector4 a)
        {
            return new Vector4(a.y, 1.0f, a.w, a.w);
        }

        public static Vector4 z1ww(this Vector4 a)
        {
            return new Vector4(a.z, 1.0f, a.w, a.w);
        }

        public static Vector4 w1ww(this Vector4 a)
        {
            return new Vector4(a.w, 1.0f, a.w, a.w);
        }

        public static Vector4 _10ww(this Vector4 a)
        {
            return new Vector4(1.0f, 0.0f, a.w, a.w);
        }

        public static Vector4 _00ww(this Vector4 a)
        {
            return new Vector4(0.0f, 0.0f, a.w, a.w);
        }

        public static Vector4 x0ww(this Vector4 a)
        {
            return new Vector4(a.x, 0.0f, a.w, a.w);
        }

        public static Vector4 y0ww(this Vector4 a)
        {
            return new Vector4(a.y, 0.0f, a.w, a.w);
        }

        public static Vector4 z0ww(this Vector4 a)
        {
            return new Vector4(a.z, 0.0f, a.w, a.w);
        }

        public static Vector4 w0ww(this Vector4 a)
        {
            return new Vector4(a.w, 0.0f, a.w, a.w);
        }

        public static Vector4 _1xww(this Vector4 a)
        {
            return new Vector4(1.0f, a.x, a.w, a.w);
        }

        public static Vector4 _0xww(this Vector4 a)
        {
            return new Vector4(0.0f, a.x, a.w, a.w);
        }

        public static Vector4 xxww(this Vector4 a)
        {
            return new Vector4(a.x, a.x, a.w, a.w);
        }

        public static Vector4 yxww(this Vector4 a)
        {
            return new Vector4(a.y, a.x, a.w, a.w);
        }

        public static Vector4 zxww(this Vector4 a)
        {
            return new Vector4(a.z, a.x, a.w, a.w);
        }

        public static Vector4 wxww(this Vector4 a)
        {
            return new Vector4(a.w, a.x, a.w, a.w);
        }

        public static Vector4 _1yww(this Vector4 a)
        {
            return new Vector4(1.0f, a.y, a.w, a.w);
        }

        public static Vector4 _0yww(this Vector4 a)
        {
            return new Vector4(0.0f, a.y, a.w, a.w);
        }

        public static Vector4 xyww(this Vector4 a)
        {
            return new Vector4(a.x, a.y, a.w, a.w);
        }

        public static Vector4 yyww(this Vector4 a)
        {
            return new Vector4(a.y, a.y, a.w, a.w);
        }

        public static Vector4 zyww(this Vector4 a)
        {
            return new Vector4(a.z, a.y, a.w, a.w);
        }

        public static Vector4 wyww(this Vector4 a)
        {
            return new Vector4(a.w, a.y, a.w, a.w);
        }

        public static Vector4 _1zww(this Vector4 a)
        {
            return new Vector4(1.0f, a.z, a.w, a.w);
        }

        public static Vector4 _0zww(this Vector4 a)
        {
            return new Vector4(0.0f, a.z, a.w, a.w);
        }

        public static Vector4 xzww(this Vector4 a)
        {
            return new Vector4(a.x, a.z, a.w, a.w);
        }

        public static Vector4 yzww(this Vector4 a)
        {
            return new Vector4(a.y, a.z, a.w, a.w);
        }

        public static Vector4 zzww(this Vector4 a)
        {
            return new Vector4(a.z, a.z, a.w, a.w);
        }

        public static Vector4 wzww(this Vector4 a)
        {
            return new Vector4(a.w, a.z, a.w, a.w);
        }

        public static Vector4 _1www(this Vector4 a)
        {
            return new Vector4(1.0f, a.w, a.w, a.w);
        }

        public static Vector4 _0www(this Vector4 a)
        {
            return new Vector4(0.0f, a.w, a.w, a.w);
        }

        public static Vector4 xwww(this Vector4 a)
        {
            return new Vector4(a.x, a.w, a.w, a.w);
        }

        public static Vector4 ywww(this Vector4 a)
        {
            return new Vector4(a.y, a.w, a.w, a.w);
        }

        public static Vector4 zwww(this Vector4 a)
        {
            return new Vector4(a.z, a.w, a.w, a.w);
        }

        public static Vector4 wwww(this Vector4 a)
        {
            return new Vector4(a.w, a.w, a.w, a.w);
        }
    }

//}
}