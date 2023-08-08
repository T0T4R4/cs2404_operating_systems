from xmlrpc.server import SimpleXMLRPCServer
from xmlrpc.server import SimpleXMLRPCRequestHandler

class MathService:
    def add(self, a, b):
        return a + b

server = SimpleXMLRPCServer(("localhost", 12345))
server.register_instance(MathService())
print("Server running...")
server.serve_forever()
