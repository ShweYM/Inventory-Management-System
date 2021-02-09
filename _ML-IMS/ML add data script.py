import time
import random
import datetime
import pandas as pd
import re
import pyodbc
import os

cnxn_str = ("Driver={SQL Server Native Client 11.0};"
            "Server=MSI\\ZYB_SERVER;"
            "Database=InventoryManagementSystem;"               #db connection config
            "Trusted_Connection=yes")

a1=(2017,1,1,0,0,0,0,0,0)              #Start time（1976-01-01 00：00：00）
a2=(2020,8,27,11,59,59,0,0,0)
a3=(0,0,7,0,0,0,0,0,0)                 #End time（1990-12-31 23：59：59）
r=5                               #Number of new data（3794）


# After running, Will get a txt file named "Script.txt", copy all the info, and paste in new sql query, click execute twice, igore the warning, then can see the new PO&DO data.




cnxn = pyodbc.connect(cnxn_str)
queryId_PO = ("SELECT TOP 1 Id FROM [InventoryManagementSystem].[dbo].[PurchaseOrders] ORDER BY [Id] DESC")
# execute the query and read to a dataframe in Python
dataId_PO = pd.read_sql(queryId_PO, cnxn)

queryId_DO = ("SELECT TOP 1 Id FROM [InventoryManagementSystem].[dbo].[DeliveryOrders] ORDER BY [Id] DESC")
dataId_DO = pd.read_sql(queryId_DO, cnxn)

queryId_DOs = ("SELECT TOP 1 Id FROM [InventoryManagementSystem].[dbo].[DeliveryOrderSupplierProducts] ORDER BY [Id] DESC")
dataId_DOs = pd.read_sql(queryId_DOs, cnxn)


ppdata = pd.read_excel(r'C:\Users\Andy\Desktop\1 - 副本.xlsx','Sheet2')
podata = pd.read_excel(r'C:\Users\Andy\Desktop\1 - 副本.xlsx','po')
pospdata = pd.read_excel(r'C:\Users\Andy\Desktop\1 - 副本.xlsx','posp')
prdata = pd.read_excel(r'C:\Users\Andy\Desktop\1 - 副本.xlsx','pp')





start=time.mktime(a1)    #生成开始时间戳
end=time.mktime(a2) #生成结束时间戳



def sindb_po(date):
    queryId_PO_s = ("SELECT COUNT(Id)  FROM [InventoryManagementSystem].[dbo].[PurchaseOrders]  WHERE POCode LIKE '%" + date + "%'")

    sn = pd.read_sql(queryId_PO_s, cnxn)
    return str(sn.values[0, 0] + 1)


def sindb_do(date):
    queryId_DO_s = ("SELECT COUNT(Id)  FROM [InventoryManagementSystem].[dbo].[DeliveryOrders]  WHERE DOCode LIKE '%" + date + "%'")

    sn = pd.read_sql(queryId_DO_s, cnxn)
    return str(sn.values[0, 0] + 1)



with open(r'po.txt','w') as f:  # 设置文件对象
    f.write("")
    f.close()

with open(r'posp.txt','w') as f:  # 设置文件对象
    f.write("")
    f.close()
with open(r'do.txt', 'w') as f:  # 设置文件对象
    f.write("")
    f.close()

with open(r'dosp.txt', 'w') as f:  # 设置文件对象
    f.write("")
    f.close()






for i in range(r):

    t=random.randint(start,end)
    #在开始和结束时间戳中随机取出一个
    t2=t+604800

    t3=t+random.randint(250000,900000)

    c=str(random.randint(1,89))
    m = str(random.randint(1, 300))
    date_touple=time.localtime(t)
    date_touple2=time.localtime(t2)
    date_touple3=time.localtime(t3)#将时间戳生成时间元组
    date1=time.strftime("%Y-%m-%dT%H:%M:%S",date_touple)
    date2 = time.strftime("%Y-%m-%dT%H:%M:%S", date_touple2)
    date3 = time.strftime("%Y-%m-%dT%H:%M:%S", date_touple3)

    if time.localtime(t).tm_mday>=10:
        day = str(time.localtime(t).tm_mday)
    else:day="0"+str(int(time.localtime(t).tm_mday))

    if time.localtime(t).tm_mon>=10:
        month = str(time.localtime(t).tm_mon)
    else:month ="0"+ str(int(time.localtime(t).tm_mon))

    if time.localtime(t).tm_year-2000>=10:
        year = str(time.localtime(t).tm_year - 2000)
    else:year = "0"+str(time.localtime(t).tm_year-2000)



    if time.localtime(t3).tm_mday>=10:
        dayD = str(time.localtime(t3).tm_mday)
    else:dayD="0"+str(int(time.localtime(t3).tm_mday))

    if time.localtime(t3).tm_mon>=10:
        monthD = str(time.localtime(t3).tm_mon)
    else:monthD ="0"+ str(int(time.localtime(t3).tm_mon))

    if time.localtime(t3).tm_year-2000>=10:
        yearD = str(time.localtime(t3).tm_year - 2000)
    else:yearD = "0"+str(time.localtime(t3).tm_year-2000)


    ikk=str(i+1+dataId_PO.values[0,0])
    ibb=str(i+2+dataId_PO.values[0,0])

    ppid=ppdata[ppdata.ProductId == int(c)].values
    ppSid = ppdata[ppdata.ProductId == int(c)].values

    prre = prdata[prdata.Id == int(c)].values

    ppidget=str(int(ppid[0,0]))
    ppsidget=str(int(ppSid[0,1]))
    ppoup=str(ppid[0,3])

    prreorder=str(int(prre[0,9]))

    with open(r'po.txt','a') as f:  # 设置文件对象
        f.write("")
        f.close()


    count = 1;
    fp=open(r'po.txt', 'r')
    for s in fp.readlines():
        li = re.findall("PO/"+day+month+year+"/", s)
        if len(li) > 0:
            count = count + len(li)

    date_PO=day+month+year

    cp=sindb_po(date_PO)


    bsd="\nINSERT [dbo].[PurchaseOrders] ([Id], [supplierId], [DeliverTo], [expectedDeliveryDate], [IssuedById], [POStatus], [POComments], [POIssueDate], [POCode]) VALUES ("+ikk+", "+ppsidget+", N'Logic University', CAST(N'"+date2+".0000000' AS DateTime2), 1, 0, N'MLTest', CAST(N'"+date1+".0000000' AS DateTime2), N'PO/"+date_PO+"/"+cp+"')"

    ksd="\nINSERT [dbo].[PurchaseOrderSupplierProducts] ([Id], [SupplierProductId], [PurchaseOrderId], [POUnitPrice], [POQuantityRequested]) VALUES ("+ibb+", "+ppidget+", "+ikk+", "+ppoup+", "+prreorder+")"

    with open(r'po.txt','a') as f:  # 设置文件对象
        f.write(bsd)
        f.close()
    with open(r'posp.txt','a') as f:  # 设置文件对象
        f.write(ksd)
        f.close()

    with open(r'do.txt','a') as f:  # 设置文件对象
        f.write("")
        f.close()

    countD = 1;
    fpD = open(r'do.txt', 'r')
    for sD in fpD.readlines():
        liD = re.findall("N'DO/"+dayD+monthD+yearD+"/", sD)
        if len(liD) > 0:
            countD = countD + len(liD)

    idd=str(i+1+dataId_DO.values[0,0])
    ids=str(i+1+dataId_DOs.values[0,0])

    date_DO=dayD+monthD+yearD

    cd=sindb_do(date_DO)

    dao="\nINSERT [dbo].[DeliveryOrders] ([Id], [DeliveryOrderNo], [SupplierId], [PurchaseOrderId], [DOComments], [DOReceivedDate], [ReceivedById], [DOCode], [DOStatus]) VALUES (" + idd +", NULL, " + ppsidget +", " + ikk +", N'MLtest-" + str(i) +"', CAST(N'" + date3 +".0000000' AS DateTime2), 27, N'DO/" + date_DO +"/" + cd + "', 1)"

    tao="\nINSERT [dbo].[DeliveryOrderSupplierProducts] ([Id], [DeliveryOrderId], [DOQuantityReceived], [PurchaseOrderSupplierProductId]) VALUES ("+ids+", "+idd+", "+prreorder+", "+ibb+")"

    with open(r'do.txt','a') as f:  # 设置文件对象
        f.write(dao)
        f.close()
    with open(r'dosp.txt','a') as f:  # 设置文件对象
        f.write(tao)
        f.close()

    end

# close the connection
del cnxn

with open(r'Script.txt','w') as b:  # 设置文件对象
    b.write("USE [InventoryManagementSystem]\nGO\n")
    b.close()


with open(r'Script.txt','a') as b:  # 设置文件对象
    b.write("\nSET IDENTITY_INSERT [dbo].[DeliveryOrders] ON ")
    b.close()

with open(r'do.txt','r') as f:  # 设置文件对象
    do=f.read()
    f.close()

with open(r'Script.txt','a') as b:  # 设置文件对象
    b.write(do)
    b.close()

with open(r'Script.txt','a') as b:  # 设置文件对象
    b.write("\nSET IDENTITY_INSERT [dbo].[DeliveryOrders] OFF\nGO\nSET IDENTITY_INSERT [dbo].[DeliveryOrderSupplierProducts] ON ")
    b.close()

with open(r'dosp.txt','r') as f:  # 设置文件对象
    dosp=f.read()
    f.close()

with open(r'Script.txt','a') as b:  # 设置文件对象
    b.write(dosp)
    b.close()

with open(r'Script.txt','a') as b:  # 设置文件对象
    b.write("\nSET IDENTITY_INSERT [dbo].[DeliveryOrderSupplierProducts] OFF\nGO")
    b.close()


with open(r'Script.txt', 'a') as b:  # 设置文件对象
    b.write("\nSET IDENTITY_INSERT [dbo].[PurchaseOrders] ON ")
    b.close()

with open(r'po.txt', 'r') as f:  # 设置文件对象
    po = f.read()
    f.close()

with open(r'Script.txt', 'a') as b:  # 设置文件对象
    b.write(po)
    b.close()

with open(r'Script.txt', 'a') as b:  # 设置文件对象
    b.write(
        "\nSET IDENTITY_INSERT [dbo].[PurchaseOrders] OFF\nGO\nSET IDENTITY_INSERT [dbo].[PurchaseOrderSupplierProducts] ON ")
    b.close()

with open(r'posp.txt', 'r') as f:  # 设置文件对象
    posp = f.read()
    f.close()

with open(r'Script.txt', 'a') as b:  # 设置文件对象
    b.write(posp)
    b.close()

with open(r'Script.txt', 'a') as b:  # 设置文件对象
    b.write("\n\nSET IDENTITY_INSERT [dbo].[PurchaseOrderSupplierProducts] OFF\nGO")
    b.close()




