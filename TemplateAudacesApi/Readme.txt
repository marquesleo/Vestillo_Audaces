﻿Configuracao no docker
I have this problem.And resolve it now.
First, login your docker account.
Second, use this command:docker images,
this command can show you all images you have, then you chose an image to push.
Third, you should add a tag for image you chose. You can use this command:
docker tag existent_image_name:latest your_user_name/new_image_name:latest
Finally, you use this command:docker push your_user_name/new_image:latest
please try it again, if denied again.You can use sudo command like this:
sudo docker push your_user_name/new_image:latest
good luck!


docker run -d -p 2520:80 -e "ConnectionStrings__MyConnectionString=Server=192.168.0.107; port=3306;user id=root;password=Leo141827;database=vestillo;pooling=true" -e "parametros__empresa=1" leopac/audacesapi --network="host"

docker run -d -p 2520:80 -e "ConnectionStrings__MyConnectionString=Server=192.168.0.107; port=3306;user id=root;password=Leo141827;database=vestillo;pooling=true" leopac/audacesapi --network="host"