#import random to get access to the library
import random

#use the random.randint function to generate a random int
n=random.randint(0,100)

#set a condition that the game will run during
runGame=True

#create a function that will control the logic of the game: if number to high print "number to high", if number to low, print "number to low"
def guessNumber():
    user_input = int(input("Enter something: "))
    #understanding types, print the type() function to get the type of your variable
    print(type(n),type(user_input))

    #understand boolean operators
    if (n<user_input):
        text="you guessed to low"
    if (n>user_input):
        text="you guessed to high"
    if (n==user_input):
        text=f"you guessed the number!!!!!!!! it was {n}!!"
        runGame=False

    print(text)

#create a while loop that will run our game inside
#understand while loop and thier conditions
while runGame==True:
    guessNumber()

    

 
guessNumber()