import xmlrpc.client

proxy = xmlrpc.client.ServerProxy("http://server_ip:12345/")
result = proxy.add(5, 7)
print("Result from remote service:", result)
