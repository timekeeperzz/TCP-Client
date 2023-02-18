import socket
import time

HOST = '   '   # The server's hostname or IP address 
PORT = 3002       # The port used by the server
HOST2 = '    '  # The server's hostname or IP address
PORT2 = 3002       # The port used by the server
HOST3 = '    '  # The server's hostname or IP address
PORT3 = 3002       # The port used by the server
HOST4 = '    '  # The server's hostname or IP address
PORT4 = 3002       # The port used by the server
 
fNew=open("data.txt","r") # The process of reading txt text
dataNew=fNew.readlines()

def test(HOST,PORT):
    
    for i in range(6): # Checking every line in data.txt        
        temp=dataNew[i]
        if dataNew[0]=="0": # If the first line in the txt file is 0, we don't want to process this line.            
break
        elif dataNew[0]=="1": # If the first line in the txt file is 1, we want to make this line 0.
            dataNew[0]="0"
            print( dataNew[0])
            continue
        elif len(temp)!=0: # Is the line blank?
            temp1= str(temp)
            temp2=temp1.replace('x00','') #filtering
            temp3=temp2.replace('\\','')
            dataNew[i]=temp3
    dataSatir1=dataNew[1]  # assigning each filled row to separate variables. there are 5 lines
    dataSatir2=dataNew[2]
    dataSatir3=dataNew[3]
    dataSatir4=dataNew[4]
    dataSatir5=dataNew[5]
    temp= dataSatir1,dataSatir2,dataSatir3,dataSatir4,dataSatir5 # sum rows in one variable    
TumSatir =str(temp)
   
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        try:
            s.settimeout(1);
            s.connect((HOST, PORT))
            message = input("Enter your command: ")
            #msg="GJD"+chr(13);
            msg=message+chr(13);

            s.send(msg.encode())
            data=s.recv(1024)
            data2=str(data)
            data3=data2.replace('x00','') #delete if there is x00 or \\ in the received message
            data4=data3.replace('\\','')
            f=open("G26_CV1_MSG.txt","w")
            f.write(data4)
            print(data4)
            message2 = input("Enter your command: ")
            #msg="GJN"+chr(13);
            msg2=message2+chr(13);
            s.send(msg2.encode())

            data5=s.recv(1024)
            data6=str(data5)
            data7=data6.replace('x00','')
            data8=data7.replace('\\','')
            f=open("G26_CV1_ETKT.txt","w")
            f.write(data8)
            print(data8)

            message3 = input("Enter your command: ")
            #msg="GPC"+chr(13);
            msg3=message3+chr(13);
            s.send(msg3.encode())
            data9=s.recv(1024)
            data10=str(data9)
            data11=data10.replace('x00','') #delete if there is x00 or \\ in the received message
            data12=data11.replace('\\','')
            data13=data12.replace('|0','')
            data14=data13.replace("b'PCS",'')
            data15=data14.replace("r'",'')
            data16=data15.replace('|','')
            
            f=open("G18_CV2.txt","w")
            f.write(data16)
            
            print(data16)
            
            textFinal=TumSatir+chr(13); 
            print(textFinal)
            s.send(textFinal.encode())  # Send the textFinal to the server
           
            
        except Exception as e:
            print("Connection Error",e)
                
            
test(HOST,PORT)
test(HOST2,PORT2)
test(HOST3,PORT3)
test(HOST4,PORT4)
