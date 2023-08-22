import threading
import time

class PrinterMonitor:
    def __init__(self):
        self.printer_semaphore = threading.Semaphore(1)

    def print_document(self, thread_id, document_name):
        print(f"Thread {thread_id} is waiting to print {document_name}.")
        with self.printer_semaphore:
            print(f"Thread {thread_id} is printing {document_name}.")
            time.sleep(2)  # Simulate printing
            print(f"Thread {thread_id} has finished printing {document_name}.")

def main():
    printer_monitor = PrinterMonitor()
    threads = []
    documents = ["DocumentA", "DocumentB", "DocumentC", "DocumentD"]

    for i in range(len(documents)):
        thread = threading.Thread(target=printer_monitor.print_document, args=(i, documents[i]))
        threads.append(thread)
        thread.start()

    for thread in threads:
        thread.join()

if __name__ == "__main__":
    main()


"""
    Output example : We see that only one thread is printing at a time, which is what we want.

Thread 0 is waiting to print DocumentA.
Thread 0 is printing DocumentA.
Thread 1 is waiting to print DocumentB.
Thread 2 is waiting to print DocumentC.
Thread 3 is waiting to print DocumentD.
Thread 0 has finished printing DocumentA.
Thread 1 is printing DocumentB.
Thread 1 has finished printing DocumentB.
Thread 2 is printing DocumentC.
Thread 2 has finished printing DocumentC.
Thread 3 is printing DocumentD.
Thread 3 has finished printing DocumentD.
"""