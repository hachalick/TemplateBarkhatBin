# به پوشه sbin بروید
cd "C:\Program Files\RabbitMQ Server\rabbitmq_server-4.2.2\sbin"

# فعال کردن پلاگین مدیریت
.\rabbitmq-plugins.bat enable rabbitmq_management

# ریستارت سرویس
.\rabbitmq-service.bat stop
.\rabbitmq-service.bat start

# یا از PowerShell
Restart-Service RabbitMQ

# صبر کنید و تست کنید
Start-Sleep -Seconds 5
Start-Process "http://localhost:15672"