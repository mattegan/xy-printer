t = linspace(0, 2*pi*20,10000);

R = [200];
r = [30];
p = [20];

R = linspace(100, 200, 3)
r = linspace(20, 50, 3)
p = linspace(10, 20, 3)


close all;
figure();
figure();
figure();
for cR = R
    for cr = r
       for cp = p
          k = cr / cR;
          l = cp / cR;
          x = ((1-k).*cos(t)) + ((l*k).*cos(((1-k)/k).*t));
          y = ((1-k).*sin(t)) - ((l*k).*sin(((1-k)/k).*t));
          figure(1);
          hold on;
          plot(t,x);
          
          figure(2);
          hold on;
          plot(t,y);
          
          figure(3);
          hold on;
          plot(x, y);
          
       end
    end
end



