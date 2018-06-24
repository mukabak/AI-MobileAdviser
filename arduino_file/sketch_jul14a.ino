int calibrationTime = 10;
long unsigned int lowIn;
long unsigned int pause = 000;
boolean lockLow = true;
boolean takeLowTime; 
int pirPin = 3;
int ledPin = 13;

void setup(){
Serial.begin(9600);
pinMode(pirPin, INPUT);
pinMode(ledPin, OUTPUT);
digitalWrite(pirPin, LOW);
for(int i = 0; i < calibrationTime; i++){
delay(1000);
}
delay(50);
}

void loop(){
if(digitalRead(pirPin) == HIGH){
digitalWrite(ledPin, HIGH);
if(lockLow){ 
lockLow = false;           
Serial.println("CustomerDetected");
delay(50);
}        
takeLowTime = true;
}
if(digitalRead(pirPin) == LOW){      
digitalWrite(ledPin, LOW);
if(takeLowTime){
lowIn = millis();
takeLowTime = false;
}

if(!lockLow && millis() - lowIn > 0){ 
lockLow = true;                       
Serial.println("NoUser");
delay(50);
}
}
}
