# Importing the libraries
import numpy as np # Math library
import matplotlib.pyplot as plt #Plotting library
import pandas as pd # Import/manage dataset library

# Importing the dataset using pandas. In this case our dataset is a CVS file
dataset = pd.read_csv('Rock_Paper_Scissors.csv')

# Implement upper confidence bound
import math
number_of_rows = 192
number_of_options = 3
number_of_selections = [0] * number_of_options
options_rewards = [0] * number_of_options

# Loop through the rows in our dataset
for n in range(0, number_of_rows):    
    ad = 0
    max_upper_bound = 0
    # Loop through the number of options we have
    for i in range(0, number_of_options):
        # If this ad has been shown to a user before, calculate the upper bound
        if number_of_selections[i] > 0:
            # Calculate the average reward up to this point
            average_reward = options_rewards[i] / number_of_selections[i]
            # Calculate the upper confidence bound
            delta_i = math.sqrt(3/2 * math.log(n + 1) / number_of_selections[i])
            upper_bound = average_reward + delta_i
        # If it hasnt been shown before, simply use a high value for the upper bound, it won't matter which one we take
        else:
            upper_bound = 1e400
        # If this option has the highest known upper bound, select it to show to the user
        if upper_bound > max_upper_bound:
            max_upper_bound = upper_bound
            ad = i
    number_of_selections[ad] = number_of_selections[ad] + 1
    
    # Check the dataset to see if we get a reward
    reward = dataset.values[n, ad] 
    options_rewards[ad] = options_rewards[ad] + reward

total_reward = sum(options_rewards)