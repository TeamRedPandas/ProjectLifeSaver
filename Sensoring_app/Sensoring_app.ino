#include <aJSON.h>
#include "SPI.h"
#include "WiFi.h"

#include "M2XStreamClient.h"

char ssid[] = "HackathonV4"; //  your network SSID (name)
char pass[] = "hackhack"; // your network password (use for WPA, or use as key for WEP)
int keyIndex = 0;            // your network key Index number (needed only for WEP)

int status = WL_IDLE_STATUS;

char deviceId[] = "74589aff45dab3e6a6a1faf1ba6620dc"; // Feed you want to post to
char m2xKey[] = "40bedc0202bdfdb32af61e91d0496a7c"; // Your M2X access key
char streamName[] = "presstime"; // Stream you want to post to

WiFiClient client;
M2XStreamClient m2xClient(&client, m2xKey);

int btn1 = 1;  // The button state
boolean pushed = false; // True when the button is pushed
int whenPushed = 0;  // The time in milliseconds from when the button is pushed.

void setup() {

    Serial.begin(9600);
    pinMode(PUSH1, INPUT_PULLUP);
    
    // attempt to connect to Wifi network:
    Serial.print("Attempting to connect to Network named: ");
    // print the network name (SSID);
    Serial.println(ssid); 
    // Connect to WPA/WPA2 network. Change this line if using open or WEP network:
    WiFi.begin(ssid, pass);
    while ( WiFi.status() != WL_CONNECTED) {
      // print dots while we wait to connect
      Serial.print(".");
      delay(300);
    }
  
    Serial.println("\nYou're connected to the network");
    Serial.println("Waiting for an ip address");
  
    while (WiFi.localIP() == INADDR_NONE) {
      // print dots while we wait for an ip addresss
      Serial.print(".");
      delay(300);
    }

    Serial.println("\nIP Address obtained");
  
    // you're connected now, so print out the status  
    printWifiStatus();
}

void loop() {
  // put your main code here, to run repeatedly: 
  
}


void printWifiStatus() {
  // print the SSID of the network you're attached to:
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print your WiFi shield's IP address:
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);
}
