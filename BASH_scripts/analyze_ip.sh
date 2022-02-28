#!/usr/bin/bash

# Analyze the IP adresses within secure logs
#
# Name: analyze_ip.sh
# Written by: Sean Condie
# Date: January 26th, 2022
# Purpose: To extract the IP adresses from failled attempts to log into the server from secure log files
# Usage: ./analyze_ip.sh #file_name(s)
# Example: ./analyze_ip.sh "/home/COIS/3380/secure*"

#
#Verify there is a file name parameter
#
if [[ $1 == *[*] ]]; then #if the parameter has a wildcard
  NAMES=( $1 ) #expand the wildcard
  NAME=${NAMES[0]} #select the first item from the wild card
else #there is no wildcard in the parameter
  NAME=$1 #assign the paramater as the name
fi

#
#Verify the name of the file to be analyzed
#
if [ -z $NAME ]; then 
  echo Please enter the name of the file as a command. #tell the user to enter a file name
  exit 1 #exit the script
elif [ -f $NAME ]; then #if the file name entered exists and is a file
  #analyze the file
  #   search for failed attempts, extract the IP, sort and count unique entries, save the top ten to a text file
  grep "Failed password for " $1 | grep -h -o -e '[0-9]\{1,3\}\.[0-9]\{1,3\}\.[0-9]\{1,3\}\.[0-9]\{1,3\}' | sort | uniq -c  | sort -n -r | head -n 10 > top_ten_ip.txt
else #if the entered file name does not exist or is not a file
  echo "The file does not exist" #tell the user the file does not exist
  exit 1 #exit the script
fi

#
#Printing the results
#
read -p "Would you like to print the results? (y/n) " response #Ask the user if they want to print the results to the screen
if [[ "$response" =~ [Yy] ]];then #if they answer 'Y' or 'y'
  echo The top ten IP addresses are:
  echo
  cat top_ten_ip.txt #print the results
  echo
fi

#
#Check the geolocation of the top results
#
FIRST_IP=$(awk 'FNR == 1 {print $2}' top_ten_ip.txt) #extract the second column from the first line
SECOND_IP=$(awk 'FNR == 2 {print $2}' top_ten_ip.txt)  #extract the second column from the second line

#geolocation of first IP
echo
wget http://ipinfo.io/$FIRST_IP #produce a file containing geolocation information
echo

#geolocation of second IP
echo
wget http://ipinfo.io/$SECOND_IP #produce a file containing geolocation information
echo 

#print the results of the first IP 
echo The results of the top IP:
echo
cat $FIRST_IP #print the contents of the first file we created
echo #new line

#print the results of the second IP
echo The results of the second top IP:
echo
cat $SECOND_IP #print the contents of the first file we created
echo

#
#end of script
#