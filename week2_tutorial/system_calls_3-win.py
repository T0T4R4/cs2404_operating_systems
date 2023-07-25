# This script illustrates systen calls in Python by drawing Mickey Mouse's face on the screen.

import ctypes
import win32api
import win32con
import win32gui

def draw_circle(x, y, radius, color):
    hwnd = win32gui.GetDesktopWindow()
    hdc = win32gui.GetWindowDC(hwnd)

    left = x - radius
    top = y - radius
    right = x + radius
    bottom = y + radius

    region = ctypes.windll.gdi32.CreateEllipticRgn(left, top, right, bottom)

    # Use GetSysColorBrush to obtain a system color brush
    brush = win32gui.GetSysColorBrush(color)
    win32gui.FillRgn(hdc, region, brush)

    win32gui.InvalidateRect(hwnd, (left, top, right, bottom), True)
    win32gui.UpdateWindow(hwnd)

    win32gui.ReleaseDC(hwnd, hdc)

if __name__ == "__main__":
    # Define the coordinates and radius for Mickey Mouse's face and ears
    face_x = 600
    face_y = 600
    face_radius = 300
    ear_radius = 130

    # Draw Mickey Mouse's face (circle in black)
    draw_circle(face_x, face_y, face_radius, win32con.COLOR_WINDOW)

    # Draw the left ear (circle in red)
    draw_circle(face_x - face_radius, face_y - face_radius, ear_radius, win32con.COLOR_HIGHLIGHT)

    # Draw the right ear (circle in blue)
    draw_circle(face_x + face_radius, face_y - face_radius, ear_radius, win32con.COLOR_HIGHLIGHT)
