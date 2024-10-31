import networkx as nx
import matplotlib.pyplot as plt
from matplotlib.patches import FancyArrowPatch

# ex1
# edges = [
#     ("D", "A"), ("D", "B"), ("D", "C"),
#     ("A", "B"), ("A", "C"),
#     ("B", "C"), ("B", "D"), 
#     ("C", "D"),
# ]

#1a
edges = [("A", "H"), ("H", "G"), ("G", "D"), 
        ("D", "C"), ("C", "B"), ("B", "G"), 
        ("G", "F"), ("F", "E"), ("E", "I"), ("I", "A") 
]

#1b
# edges = [("A", "B"), ("A", "D"), ("B", "F"), ("B", "C"), ("C", "E"), ("C", "F"), ("D", "E"), ("D", "F") ]

#1c
# edges = [
#     ("I", "F"), ("I", "G"), ("I", "H"), 
#     ("F", "E"), ("F", "G"), 
#     ("E", "F"), ("E", "G"), ("E", "H"),
#     ("G", "H"), ("E", "G"), ("E", "H"),
# ]

#1d
# edges = [
#     ("N", "K"), ("N", "L"), ("N", "M"), 
#     ("J", "K"),  
#     ("K", "N"), ("K", "L"), ("K", "M"),
#     ("L", "M"), 
# ]


# Create a multigraph
G = nx.MultiGraph()
G.add_edges_from(edges)

# Draw the graph
plt.figure(figsize=(8, 6))
pos = nx.spring_layout(G)  # Layout for better visualization

# Draw nodes
nx.draw_networkx_nodes(G, pos, node_size=700, node_color="skyblue")

# Draw labels
nx.draw_networkx_labels(G, pos, font_size=12, font_weight="bold")

# Draw curved edges without using `connectionstyle`
for (u, v) in G.edges():
    # Create a curved edge from u to v
    edge1 = FancyArrowPatch(posA=pos[u], posB=pos[v], connectionstyle="arc3,rad=0.2", 
                            color='gray', alpha=0.5, linewidth=2)
    plt.gca().add_patch(edge1)

    # Create a curved edge in the opposite direction from v to u
    edge2 = FancyArrowPatch(posA=pos[v], posB=pos[u], connectionstyle="arc3,rad=-0.2", 
                            color='gray', alpha=0.5, linewidth=2)
    plt.gca().add_patch(edge2)

plt.title("Undirected Multigraph with Curved Edges in Both Directions")
plt.axis('off')  # Turn off the axis

# Save the image to a file
plt.savefig("curved_multigraph_edges.png")  
plt.close()  
