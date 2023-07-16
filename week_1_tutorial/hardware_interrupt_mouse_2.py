import threading
import time
from pynput import mouse

# Global variables
mouse_clicked = False
mouse_x = None
mouse_y = None
stop_threads = False

# Simulated first task
def task1():
    while not stop_threads:
        print("Task 1 running")
        time.sleep(1)

# Simulated second task triggered by a mouse click interrupt
def task2():
    global mouse_clicked, mouse_x, mouse_y
    while not stop_threads:
        if mouse_clicked:
            print("Mouse click interrupt received (Task 2)")
            print(f"Mouse coordinates: X = {mouse_x}, Y = {mouse_y}")
            mouse_clicked = False
        time.sleep(0.1)

# Mouse click callback function
def on_mouse_click(x, y, button, pressed):
    global mouse_clicked, mouse_x, mouse_y
    if not pressed:
        mouse_x, mouse_y = x, y
        mouse_clicked = True

# Start the first task in a separate thread
thread1 = threading.Thread(target=task1)
thread1.start()

# Start the second task in a separate thread
thread2 = threading.Thread(target=task2)
thread2.start()

# Create a mouse listener
mouse_listener = mouse.Listener(on_click=on_mouse_click)
mouse_listener.start()

try:
    # Keep the main thread running
    while True:
        time.sleep(1)

except KeyboardInterrupt:
    # Set the stop_threads flag to True to terminate the threads
    stop_threads = True
    thread1.join()
    thread2.join()
