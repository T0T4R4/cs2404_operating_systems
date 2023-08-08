from pynput.mouse import Listener

def on_move(x, y):
    print(f"Mouse moved to ({x}, {y})")

# Create a listener to monitor mouse movements
listener = Listener(on_move=on_move)

# Start the listener to begin monitoring
listener.start()

# The main program continues execution
while True:
    # Perform other tasks or operations here
    pass
